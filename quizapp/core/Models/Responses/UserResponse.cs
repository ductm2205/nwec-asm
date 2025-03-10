namespace core.Models.Responses;

public class UserResponse : IResponse
{
    public Guid Id { get; set; }
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public string DisplayName => FirstName + " " + LastName;

    public required string Email { get; set; }

    public DateTime DateOfBirth { get; set; }
    public string? Avatar { get; set; }

    public required bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
