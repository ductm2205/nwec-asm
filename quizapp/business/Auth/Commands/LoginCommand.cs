using System;
using business.Commands;
using core.Models.Responses.Auth;

namespace business.Auth.Commands;

public class LoginCommand : BaseCommand<LoginResponse>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
