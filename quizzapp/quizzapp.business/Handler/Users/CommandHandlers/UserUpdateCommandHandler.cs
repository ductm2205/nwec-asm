using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Users.Commands;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Users.CommandHandlers;

public class UserUpdateCommandHandler : BaseCommandHandler<UserUpdateCommand, bool>
{
    public UserUpdateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.DateOfBirth = request.DateOfBirth;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.UserRepository.Update(user);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
