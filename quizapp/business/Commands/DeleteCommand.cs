namespace business.Commands;

public class DeleteCommand : BaseCommand<bool>, IHasIdCommand<bool>
{
    public Guid Id { get; set; }
    public bool HardDelete { get; set; } = false;
}
