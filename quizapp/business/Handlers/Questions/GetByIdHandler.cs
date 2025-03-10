using System;
using business.Commands;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Questions;

public class GetByIdHandler : BaseHandler<GetByIdCommand<QuestionResponse>, QuestionResponse>
{
    public GetByIdHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<QuestionResponse> HandleCommand(GetByIdCommand<QuestionResponse> request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        var res = new QuestionResponse()
        {
            Id = ques.Id,
            Content = ques.Content,
            QuestionType = ques.QuestionType,
            IsActive = ques.IsActive,
            QuizId = ques.QuizId,
            CreatedAt = ques.CreatedAt,
            UpdatedAt = ques.UpdatedAt
        };

        return res;
    }
}
