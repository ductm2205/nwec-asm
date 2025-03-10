using core.Models.Responses;

namespace business.Commands;

public class GetByIdCommand<T> : BaseCommand<T>, IHasIdCommand<T> where T : class, IResponse
{
    public Guid Id { get; set; }
}
