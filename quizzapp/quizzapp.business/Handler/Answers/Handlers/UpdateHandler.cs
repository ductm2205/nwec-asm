using System;
using quizzapp.business.Handler.Answers.Commands;
using quizzapp.business.Handler.Base;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;

namespace quizzapp.business.Handler.Answers.Handlers;

public class UpdateHandler : BaseCommandHandler<UpdateCommand, bool>
{
    public UpdateHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var target = await _unitOfWork.AnswerRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        target.Content = request.Content;
        target.IsCorrect = request.IsCorrect;

        target.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.AnswerRepository.Update(target);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
