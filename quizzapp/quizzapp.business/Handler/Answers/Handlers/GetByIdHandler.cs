using System;
using quizzapp.business.Handler.Answers.Commands;
using quizzapp.business.Handler.Base;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Answers.Handlers;

public class GetByIdHandler : BaseCommandHandler<GetByIdCommand, Answer>
{
    public GetByIdHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<Answer> HandleCommand(GetByIdCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AnswerRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();
    }
}
