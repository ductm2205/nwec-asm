using System;
using core.Models;
using core.Models.Responses;

namespace business.Commands.Users;

public class GetAllCommand : BaseCommand<PaginatedResult<UserResponse>>
{

}
