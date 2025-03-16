using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Users;

public class GetAllHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) : BaseHandler<GetAllCommand<UserResponse>, PaginatedResult<UserResponse>>(unitOfWork)
{
    private readonly UserManager<User> _userManager = userManager;

    protected override async Task<PaginatedResult<UserResponse>> HandleCommand(GetAllCommand<UserResponse> request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepo.GetAllAsync();

        var userResponses = new List<UserResponse>();

        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var response = new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.DisplayName,
                Email = user.Email!,
                UserName = user.UserName!,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                Roles = roles
            };

            userResponses.Add(response);
        }

        return new PaginatedResult<UserResponse>(
            pageNumber: 1,
            pageSize: userResponses.Count,
            totalCount: userResponses.Count,
            items: userResponses
        );
    }
}
