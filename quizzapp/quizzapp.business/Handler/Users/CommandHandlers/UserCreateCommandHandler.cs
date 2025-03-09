using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Users.Commands;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Users.CommandHandlers;

public class UserCreateCommandHandler : BaseCommandHandler<UserCreateCommand, User>
{
    public UserCreateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<User> HandleCommand(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        await _unitOfWork.UserRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        return user;
    }
}
