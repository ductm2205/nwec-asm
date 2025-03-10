using System;

namespace models.Base;

public class BaseEntity : BaseItem, IHasId, IHasIsActive
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
}