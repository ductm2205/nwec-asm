namespace quizzapp.model.Base;

public interface IEntity
{
    Guid Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    DateTime DeletedAt { get; set; }
    bool IsDeleted { get; set; }
    bool IsActive { get; set; }
}