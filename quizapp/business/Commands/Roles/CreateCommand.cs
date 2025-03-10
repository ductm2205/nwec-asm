using System;

namespace business.Commands.Roles;

public class CreateCommand : BaseCommand<bool>
{
    public required string Description { get; set; }
    public required bool IsActive { get; set; } = true;
}
