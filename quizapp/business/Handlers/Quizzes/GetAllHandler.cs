using System;
using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class GetAllHandler : BaseHandler<GetAllCommand<QuizResponse>, PaginatedResult<QuizResponse>>
{
    public GetAllHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<PaginatedResult<QuizResponse>> HandleCommand(GetAllCommand<QuizResponse> request, CancellationToken cancellationToken)
    {
        var quizzes = await _unitOfWork.QuizRepo.GetAllAsync();

        var quizResponses = quizzes.Select(user => new QuizResponse
        {
            Id = user.Id,
            Title = user.Title,
            Description = user.Description,
            Duration = user.Duration,
            ThumbnailUrl = user.ThumbnailUrl,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        }).ToList();

        return new PaginatedResult<QuizResponse>(
            pageNumber: 1,
            pageSize: quizResponses.Count,
            totalCount: quizResponses.Count,
            items: quizResponses
        );
    }
}
