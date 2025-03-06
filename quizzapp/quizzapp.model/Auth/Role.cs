using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using quizzapp.model.Base;
using quizzapp.model.Relation;

namespace quizzapp.model.Auth;

public class Role : IdentityRole<Guid>, IEntity
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    [Required]
    public required bool IsActive { get; set; } = true;
}
