using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Users.Commands;

public class UserDeleteCommand : BaseCommand<bool>
{
    [Required(ErrorMessage = "User ID is required")]
    public Guid Id { get; set; }
}
