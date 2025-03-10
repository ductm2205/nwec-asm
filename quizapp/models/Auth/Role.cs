using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using models.Base;

namespace models.Auth;

[Table("roles", Schema = "auth")]
public class Role : IdentityRole<Guid>, IHasIsActive, IBaseItem
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Description { get; set; }

    [Required]
    public required bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
}
