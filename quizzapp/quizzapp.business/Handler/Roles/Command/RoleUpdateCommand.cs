using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;

namespace quizzapp.business.Handler.Roles.Command;

public class RoleUpdateCommand : BaseCommand<bool>
{
    [Required(ErrorMessage = "Role ID is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Role name is required")]
    [StringLength(50, ErrorMessage = "Role name cannot exceed 50 characters")]
    public required string Name { get; set; }

    [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
    public string? Description { get; set; }
}