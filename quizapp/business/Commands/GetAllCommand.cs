using core.Models;
using core.Models.Responses;

namespace business.Commands;

public class GetAllCommand<T> : BaseCommand<PaginatedResult<T>> where T : class, IResponse
{

}
