using System;
using business.Commands;
using business.Commands.Roles;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;
using models.Auth;

namespace business.Handlers.Roles;

public class GetAllHandler : BaseHandler<GetAllCommand<RoleResponse>, PaginatedResult<RoleResponse>>
{
    public GetAllHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<PaginatedResult<RoleResponse>> HandleCommand(GetAllCommand<RoleResponse> request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.RoleRepo.GetAllAsync();

        var roleResponses = users.Select(user => new RoleResponse
        {
            Id = user.Id,
            Description = user.Description,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        }).ToList();

        return new PaginatedResult<RoleResponse>(
            pageNumber: 1,
            pageSize: roleResponses.Count,
            totalCount: roleResponses.Count,
            items: roleResponses
        );
    }
}
