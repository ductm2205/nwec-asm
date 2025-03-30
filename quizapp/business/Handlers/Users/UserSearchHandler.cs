using System;
using business.Commands;
using business.Commands.Users;
using core.Extensions;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using models.Auth;

namespace business.Handlers.Users;

public class UserSearchHandler : BaseHandler<UserSearchQuery, PaginatedResult<UserResponse>>
{
    private readonly UserManager<User> _userManager;

    public UserSearchHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
    {
        _userManager = userManager;
    }

    protected override async Task<PaginatedResult<UserResponse>> HandleCommand(UserSearchQuery request, CancellationToken cancellationToken)
    {
        var users = _unitOfWork.UserRepo.GetQuery();

        // Apply keyword filter
        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            users = users.Where(u => u.FirstName.Contains(request.Keyword) || u.LastName.Contains(request.Keyword));
        }

        // Apply role filter
        if (!string.IsNullOrWhiteSpace(request.RoleName))
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(request.RoleName);
            Console.WriteLine($"Users in role {request.RoleName}: {usersInRole.Count}");
            var userIdsInRole = usersInRole.Select(u => u.Id).ToHashSet();
            users = users.Where(u => userIdsInRole.Contains(u.Id));
        }

        // Apply active/deleted filters
        if (request.IncludeInactive.HasValue)
        {
            users = users.Where(u => u.IsActive == request.IncludeInactive.Value);
        }
        else
        {
            users = users.Where(u => u.IsActive); // Default to active only
        }

        if (request.IncludeDeleted.HasValue)
        {
            users = users.Where(u => u.IsDeleted == request.IncludeDeleted.Value);
        }
        else
        {
            users = users.Where(u => !u.IsDeleted); // Default to non-deleted
        }

        // Calculate total count
        var totalCount = await users.CountAsync(cancellationToken);

        // // Apply ordering
        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            var direction = request.OrderDirection == OrderDirection.ASC ? "asc" : "desc";
            users = users.OrderByExtension(request.OrderBy, direction);
        }
        else
        {
            users = users.OrderBy(x => x.CreatedAt);
        }

        // Apply pagination
        users = users.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize);

        // Materialize the query - avoid conflict database read
        var res = await users.ToListAsync(cancellationToken);
        Console.WriteLine($"Users found after filters: {res.Count}");

        var userResponses = new List<UserResponse>();

        foreach (var user in res)
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
                Roles = [.. roles]
            };
            userResponses.Add(response);
        }

        return new PaginatedResult<UserResponse>(
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            totalCount: totalCount,
            items: userResponses
        );
    }
}