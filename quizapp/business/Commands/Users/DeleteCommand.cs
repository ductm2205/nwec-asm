using System;

namespace business.Commands.Users;

public class DeleteCommand : BaseCommand<bool>, IHasIdCommand<bool>
{
    public Guid Id { get; set; }
}
