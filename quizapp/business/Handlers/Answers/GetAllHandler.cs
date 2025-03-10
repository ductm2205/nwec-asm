using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Answers;

public class GetAllHandler(IUnitOfWork unitOfWork) : BaseHandler<GetAllCommand<AnswerResponse>, PaginatedResult<AnswerResponse>>(unitOfWork)
{
    protected override async Task<PaginatedResult<AnswerResponse>> HandleCommand(GetAllCommand<AnswerResponse> request, CancellationToken cancellationToken)
    {
        var answers = await _unitOfWork.AnswerRepo.GetAllAsync();

        var ansResponses = answers.Select(ques => new AnswerResponse
        {
            Id = ques.Id,
            Content = ques.Content,
            IsCorrect = ques.IsCorrect,
            IsActive = ques.IsActive,
            QuestionId = ques.QuestionId,
            CreatedAt = ques.CreatedAt,
            UpdatedAt = ques.UpdatedAt
        }).ToList();

        return new PaginatedResult<AnswerResponse>(
            pageNumber: 1,
            pageSize: ansResponses.Count,
            totalCount: ansResponses.Count,
            items: ansResponses
        );
    }
}
