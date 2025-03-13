using business.Commands;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;
using Microsoft.EntityFrameworkCore;

namespace business.Handlers.Questions;

public class GetByIdHandler(IUnitOfWork unitOfWork) : BaseHandler<GetByIdCommand<QuestionResponse>, QuestionResponse>(unitOfWork)
{
    protected override async Task<QuestionResponse> HandleCommand(GetByIdCommand<QuestionResponse> request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepo.GetQuery(q => q.Id == request.Id)
            .Include(q => q.Answers)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new EntityNotFoundException();

        var ans = ques.Answers!.Select(e => new AnswerResponse
        {
            Id = e.Id,
            Content = e.Content,
            IsCorrect = e.IsCorrect,
            CreatedAt = e.CreatedAt,
            UpdatedAt = e.UpdatedAt,
            IsActive = e.IsActive,
        });

        var res = new QuestionResponse()
        {
            Id = ques.Id,
            Content = ques.Content,
            QuestionType = ques.QuestionType,
            Answers = ans,
            IsActive = ques.IsActive,
            QuizId = ques.QuizId,
            CreatedAt = ques.CreatedAt,
            UpdatedAt = ques.UpdatedAt
        };

        return res;
    }
}
