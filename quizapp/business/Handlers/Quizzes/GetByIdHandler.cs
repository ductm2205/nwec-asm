using System;
using business.Commands;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;
using models.Common;

namespace business.Handlers.Quizzes;

public class GetByIdHandler : BaseHandler<GetByIdCommand<QuizResponse>, QuizResponse>
{
    public GetByIdHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<QuizResponse> HandleCommand(GetByIdCommand<QuizResponse> request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        var res = new QuizResponse()
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            Duration = quiz.Duration,
            IsActive = quiz.IsActive,
            CreatedAt = quiz.CreatedAt,
            UpdatedAt = quiz.UpdatedAt
        };

        return res;
    }
}

