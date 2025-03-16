using System.ComponentModel.DataAnnotations;
using business.Commands.Users;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Users;

public class CreateHandler : BaseHandler<CreateCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public CreateHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
    {
        _userManager = userManager;
    }

    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        if (request.Password != request.ConfirmPassword)
        {
            throw new ValidationException("Password and confirmation password do not match.");
        }

        var pwHasher = new PasswordHasher<User>();


        var user = new User
        {
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            Email = request.Email,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            DateOfBirth = request.DateOfBirth,
            IsActive = request.IsActive,
            EmailConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };

        var hashedPassword = pwHasher.HashPassword(user, request.Password);
        var result = await _userManager.CreateAsync(user, hashedPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException($"Failed to create user: {errors}");
        }

        await _userManager.AddToRoleAsync(user, "User");

        return true;
    }
}
