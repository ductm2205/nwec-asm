using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Users.Commands;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Users.CommandHandlers;

public class UserDeleteCommandHandler : BaseCommandHandler<UserDeleteCommand, bool>
{
    public UserDeleteCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id);
        if (user == null) return false;

        _unitOfWork.UserRepository.Delete(user);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
