using core.Models.Responses;

namespace business.Commands.Users;

public class GetByIdCommand : BaseCommand<UserResponse>, IHasIdCommand<UserResponse>
{
    public Guid Id { get; set; }
}
