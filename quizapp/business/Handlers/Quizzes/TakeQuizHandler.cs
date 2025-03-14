using business.Commands.Quizzes;
using core.Exceptions;
using core.Models.Responses;
using core.Models.Responses.Quizzes;
using data.Infrastructures;
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
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.QuizId) ?? throw new EntityNotFoundException();

        var userQuiz = new UserQuiz
        {
            QuizCode = Guid.NewGuid(),
            UserId = user.Id,
            QuizId = quiz.Id,
            CreatedAt = DateTime.UtcNow,
            IsDeleted = false,
        };

        var resp = new QuizForTestResponse
        {
            Id = Guid.NewGuid(),
            Title = quiz.Title,
            Description = quiz.Description,
            QuizCode = userQuiz.QuizCode,
            StartTime = DateTime.UtcNow,
            Duration = 45,
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
