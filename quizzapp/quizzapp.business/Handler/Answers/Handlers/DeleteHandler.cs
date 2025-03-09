using System;
using quizzapp.business.Handler.Answers.Commands;
using quizzapp.business.Handler.Base;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;

namespace quizzapp.business.Handler.Answers.Handlers;

public class DeleteHandler : BaseCommandHandler<DeleteCommand, bool>
{
    public DeleteHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
    {
        var target = await _unitOfWork.AnswerRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        _unitOfWork.AnswerRepository.Delete(target);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
