using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Roles;

public class GetAllHandler(IUnitOfWork unitOfWork) : BaseHandler<GetAllCommand<RoleResponse>, PaginatedResult<RoleResponse>>(unitOfWork)
{
    protected override async Task<PaginatedResult<RoleResponse>> HandleCommand(GetAllCommand<RoleResponse> request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.RoleRepo.GetAllAsync();

        var roleResponses = roles.Select(role => new RoleResponse
        {
            Id = role.Id,
            Description = role.Description,
            IsActive = role.IsActive,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt
        }).ToList();

        return new PaginatedResult<RoleResponse>(
            pageNumber: 1,
            pageSize: roleResponses.Count,
            totalCount: roleResponses.Count,
            items: roleResponses
        );
    }
}
