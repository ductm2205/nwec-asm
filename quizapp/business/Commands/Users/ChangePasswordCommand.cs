using System;

namespace business.Commands.Users;

public class ChangePasswordCommand : BaseCommand<bool>
{
    public Guid Id { get; set; }

    public string? UserName { get; set; }
    public string? CurrentPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}
