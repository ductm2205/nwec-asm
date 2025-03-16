using System;
using System.ComponentModel.DataAnnotations;
using business.Commands.Users;
using core.Exceptions;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Users;

public class ChangePasswordHandler : BaseHandler<ChangePasswordCommand, bool>
{
    private readonly UserManager<User> _userManager;
    public ChangePasswordHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
    {
        _userManager = userManager;
    }

    protected override async Task<bool> HandleCommand(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword!);

        var hasher = new PasswordHasher<User>();
        var hashedpw = hasher.HashPassword(user, request.CurrentPassword!);
        System.Console.WriteLine($"Hashed current: {hashedpw}");

        System.Console.WriteLine($"User current: {user.PasswordHash}");

        System.Console.WriteLine("Compare: {0}", string.Equals(hashedpw, user.PasswordHash));
        if (!isCurrentPasswordValid)
        {
            throw new ValidationException("Current password is incorrect.");
        }

        // Change the password
        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword!, request.NewPassword!);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException($"Failed to change password: {errors}");
        }

        return result.Succeeded;
    }
}
