using business.Commands;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using models.Auth;

namespace business.Handlers.Users;

public class GetByIdHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) : BaseHandler<GetByIdCommand<UserResponse>, UserResponse>(unitOfWork)
{
    private readonly UserManager<User> _userManager = userManager;
    protected override async Task<UserResponse> HandleCommand(GetByIdCommand<UserResponse> request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        var roles = await _userManager.GetRolesAsync(user);

        var res = new UserResponse()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email!,
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber!,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth,
            Roles = roles
        };

        return res;
    }
}
