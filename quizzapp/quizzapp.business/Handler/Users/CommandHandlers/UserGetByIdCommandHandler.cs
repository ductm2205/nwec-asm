using System;
using Microsoft.EntityFrameworkCore;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Users.Commands;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Users.CommandHandlers;

public class UserGetByIdCommandHandler : BaseCommandHandler<UserGetByIdCommand, User>
{
    public UserGetByIdCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<User> HandleCommand(UserGetByIdCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.UserRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();
    }
}
