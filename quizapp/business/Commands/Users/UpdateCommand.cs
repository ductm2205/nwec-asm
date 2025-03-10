namespace business.Commands.Users;

public class UpdateCommand : CreateCommand, IHasIdCommand<bool>
{
    public Guid Id { get; set; }
    public bool IsActive { get; set; }
}
