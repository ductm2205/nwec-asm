using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace core.Models.Responses;

public class UserResponse : IResponse
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string DisplayName { get; set; }

    public required string Email { get; set; }

    public required string UserName { get; set; }
    public required string PhoneNumber { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string? Avatar { get; set; }

    public required bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<IdentityUserRole<Guid>> Roles { get; set; } = [];
}
