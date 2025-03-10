using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class GetAllHandler(IUnitOfWork unitOfWork) : BaseHandler<GetAllCommand<QuizResponse>, PaginatedResult<QuizResponse>>(unitOfWork)
{
    protected override async Task<PaginatedResult<QuizResponse>> HandleCommand(GetAllCommand<QuizResponse> request, CancellationToken cancellationToken)
    {
        var quizzes = await _unitOfWork.QuizRepo.GetAllAsync();

        var quizResponses = quizzes.Select(quiz => new QuizResponse
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            Duration = quiz.Duration,
            ThumbnailUrl = quiz.ThumbnailUrl,
            IsActive = quiz.IsActive,
            CreatedAt = quiz.CreatedAt,
            UpdatedAt = quiz.UpdatedAt
        }).ToList();

        return new PaginatedResult<QuizResponse>(
            pageNumber: 1,
            pageSize: quizResponses.Count,
            totalCount: quizResponses.Count,
            items: quizResponses
        );
    }
}
