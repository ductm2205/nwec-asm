using System;

namespace models.Base;

public class BaseEntity : IBaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsActive { get; set; }
}

public interface IBaseEntity1
{
    Guid Id { get; set; }
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
    DateTime DeletedAt { get; set; }
    bool IsDeleted { get; set; }
    bool IsActive { get; set; }
}
