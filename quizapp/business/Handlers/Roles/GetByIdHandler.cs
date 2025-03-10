using System;
using business.Commands;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Roles;

public class GetByIdHandler(IUnitOfWork unitOfWork) : BaseHandler<GetByIdCommand<RoleResponse>, RoleResponse>(unitOfWork)
{
    protected override async Task<RoleResponse> HandleCommand(GetByIdCommand<RoleResponse> request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        var res = new RoleResponse()
        {
            Id = role.Id,
            Description = role.Description,
            IsActive = role.IsActive,
            CreatedAt = role.CreatedAt,
            UpdatedAt = role.UpdatedAt
        };

        return res;
    }
}
