using business.Commands.Quizzes;
using core.Exceptions;
using core.Models.Responses;
using core.Models.Responses.Quizzes;
using data.Infrastructures;
using Microsoft.EntityFrameworkCore;
using models.Relationship;

namespace business.Handlers.Quizzes;

public class TakeQuizHandler : BaseHandler<TakeQuizCommand, QuizForTestResponse>
{
    public TakeQuizHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<QuizForTestResponse> HandleCommand(TakeQuizCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.UserId) ?? throw new EntityNotFoundException();
        var quiz = await _unitOfWork.QuizRepo.GetQuery(q => q.Id == request.QuizId)
        .Include(q => q.Questions!)
            .ThenInclude(q => q.Answers)
        .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new EntityNotFoundException();

        var userQuiz = await _unitOfWork.UserQuizRepo.GetQuery(
            uq =>
                uq.QuizCode == request.QuizCode
                && uq.UserId == user.Id
                && uq.QuizId == quiz.Id)
        .FirstOrDefaultAsync(cancellationToken: cancellationToken)
        ?? throw new EntityNotFoundException();

        userQuiz.StartedAt = DateTime.UtcNow;
        _unitOfWork.UserQuizRepo.Update(userQuiz);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var resp = new QuizForTestResponse
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            QuizCode = userQuiz.QuizCode,
            StartTime = DateTime.UtcNow,
            Duration = quiz.Duration,
            Questions = [.. quiz.Questions!.Select(
                ques => new QuestionResponse {
                    Id = ques.Id,
                    Content = ques.Content,
                    QuestionType = ques.QuestionType,
                    Answers = [.. ques.Answers!.Select(
                        ans => new AnswerResponse
                        {
                            Id = ans.Id,
                            Content = ans.Content
                        })]
            })]
        };

        return resp;
    }

}
