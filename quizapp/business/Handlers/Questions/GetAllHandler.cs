using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;
using Microsoft.EntityFrameworkCore;

namespace business.Handlers.Questions;

public class GetAllHandler(IUnitOfWork unitOfWork) : BaseHandler<GetAllCommand<QuestionResponse>, PaginatedResult<QuestionResponse>>(unitOfWork)
{
    protected override async Task<PaginatedResult<QuestionResponse>> HandleCommand(GetAllCommand<QuestionResponse> request, CancellationToken cancellationToken)
    {
        var questions = _unitOfWork.QuestionRepo.GetQuery().Include(q => q.Answers);

        var quesResponses = await questions.Select(ques => new QuestionResponse
        {
            Id = ques.Id,
            Content = ques.Content,
            QuestionType = ques.QuestionType,
            QuizId = ques.QuizId,
            Answers = ques.Answers!.Select(
                a => new AnswerResponse
                {
                    Id = a.Id,
                    Content = a.Content,
                    IsActive = a.IsActive,
                    CreatedAt = a.CreatedAt,
                    UpdatedAt = a.UpdatedAt
                }
            ),
            IsActive = ques.IsActive,
            CreatedAt = ques.CreatedAt,
            UpdatedAt = ques.UpdatedAt
        }).ToListAsync(cancellationToken: cancellationToken);

        return new PaginatedResult<QuestionResponse>(
            pageNumber: 1,
            pageSize: quesResponses.Count,
            totalCount: quesResponses.Count,
            items: quesResponses
        );
    }
}
