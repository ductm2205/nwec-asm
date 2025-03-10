namespace core.Models.Responses;

public class RoleResponse : IResponse
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}