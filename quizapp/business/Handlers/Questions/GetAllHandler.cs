using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Questions;

public class GetAllHandler(IUnitOfWork unitOfWork) : BaseHandler<GetAllCommand<QuestionResponse>, PaginatedResult<QuestionResponse>>(unitOfWork)
{
    protected override async Task<PaginatedResult<QuestionResponse>> HandleCommand(GetAllCommand<QuestionResponse> request, CancellationToken cancellationToken)
    {
        var questions = await _unitOfWork.QuestionRepo.GetAllAsync();

        var quesResponses = questions.Select(ques => new QuestionResponse
        {
            Id = ques.Id,
            Content = ques.Content,
            QuestionType = ques.QuestionType,
            QuizId = ques.QuizId,
            IsActive = ques.IsActive,
            CreatedAt = ques.CreatedAt,
            UpdatedAt = ques.UpdatedAt
        }).ToList();

        return new PaginatedResult<QuestionResponse>(
            pageNumber: 1,
            pageSize: quesResponses.Count,
            totalCount: quesResponses.Count,
            items: quesResponses
        );
    }
}
