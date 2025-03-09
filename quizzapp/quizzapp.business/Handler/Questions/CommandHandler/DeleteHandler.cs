using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Questions.Command;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;

namespace quizzapp.business.Handler.Questions.CommandHandler;

public class DeleteHandler : BaseCommandHandler<QuestionDeleteCommand, bool>
{
    public DeleteHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(QuestionDeleteCommand request, CancellationToken cancellationToken)
    {
        var target = await _unitOfWork.QuestionRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        _unitOfWork.QuestionRepository.Delete(target);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
