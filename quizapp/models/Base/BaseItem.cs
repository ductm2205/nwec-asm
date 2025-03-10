using System;

namespace models.Base;

public class BaseItem : IBaseItem
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
