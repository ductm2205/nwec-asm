using System;
using business.Commands.Roles;
using data.Infrastructures;
using models.Auth;

namespace business.Handlers.Roles;

public class CreateHandler(IUnitOfWork unitOfWork) : BaseHandler<CreateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var newR = new Role
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            IsActive = request.IsActive,
        };

        _unitOfWork.RoleRepo.Add(newR);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
