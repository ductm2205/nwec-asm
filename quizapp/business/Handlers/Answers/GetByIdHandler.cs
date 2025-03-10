using business.Commands;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Answers;

public class GetByIdHandler(IUnitOfWork unitOfWork) : BaseHandler<GetByIdCommand<AnswerResponse>, AnswerResponse>(unitOfWork)
{
    protected override async Task<AnswerResponse> HandleCommand(GetByIdCommand<AnswerResponse> request, CancellationToken cancellationToken)
    {
        var ans = await _unitOfWork.AnswerRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        var res = new AnswerResponse()
        {
            Id = ans.Id,
            Content = ans.Content,
            IsCorrect = ans.IsCorrect,
            IsActive = ans.IsActive,
            QuestionId = ans.QuestionId,
            CreatedAt = ans.CreatedAt,
            UpdatedAt = ans.UpdatedAt
        };

        return res;
    }
}
