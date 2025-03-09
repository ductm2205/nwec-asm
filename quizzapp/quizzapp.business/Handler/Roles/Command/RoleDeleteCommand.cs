using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;

namespace quizzapp.business.Handler.Roles.Command;

public class RoleDeleteCommand : BaseCommand<bool>
{
    [Required(ErrorMessage = "Role ID is required")]
    public Guid Id { get; set; }
}