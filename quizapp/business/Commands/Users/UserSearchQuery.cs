using System;
using core.Models.Responses;
using models.Auth;

namespace business.Commands.Users;

public class UserSearchQuery : BaseSearchQuery<UserResponse>
{
    public string? RoleName { get; set; }
}
