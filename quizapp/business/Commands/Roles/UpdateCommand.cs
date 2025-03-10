using System;

namespace business.Commands.Roles;

public class UpdateCommand : CreateCommand, IHasIdCommand<bool>
{
    public Guid Id { get; set; }
}
