using System.ComponentModel.DataAnnotations;
using business.Commands.Users;
using core.Exceptions;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Users;

public class UpdateHandler : BaseHandler<UpdateCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public UpdateHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
    {
        _userManager = userManager;
    }

    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.FirstName = request.FirstName!;
        user.LastName = request.LastName!;
        user.DateOfBirth = request.DateOfBirth;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException($"Failed to update user: {errors}");
        }

        if (request.Roles != null)
        {
            foreach (var role in request.Roles)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (!currentRoles.Contains(role))
                {
                    // Add new role
                    var roleResult = await _userManager.AddToRoleAsync(user, role);

                    if (!roleResult.Succeeded)
                    {
                        var errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                        throw new ValidationException($"Failed to update user role: {errors}");
                    }
                }
            }
        }

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
