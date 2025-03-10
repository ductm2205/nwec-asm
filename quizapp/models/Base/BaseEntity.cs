using System.ComponentModel.DataAnnotations;

namespace models.Base;

public class BaseEntity : BaseItem, IHasId, IHasIsActive
{
    [Required]
    public Guid Id { get; set; }
    public bool IsActive { get; set; } = true;
}