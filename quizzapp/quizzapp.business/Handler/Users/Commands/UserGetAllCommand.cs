using System;
using quizzapp.business.Handler.Base;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Users.Commands;

public class UserGetAllCommand : BaseCommand<IEnumerable<User>>
{
    public bool IncludeInactive { get; set; } = false;
}
