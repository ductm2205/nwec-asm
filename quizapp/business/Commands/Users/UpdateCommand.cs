using System;
using System.ComponentModel.DataAnnotations;

namespace business.Commands.Users;

public class UpdateCommand : CreateCommand, IHasIdCommand<bool>
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Active status is required")]
    public bool IsActive { get; set; }
}
