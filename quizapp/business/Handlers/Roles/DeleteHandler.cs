using System;
using business.Commands;
using data.Infrastructures;

namespace business.Handlers.Roles;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<DeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepo.GetByIdAsync(request.Id);
        if (role == null) return false;

        _unitOfWork.RoleRepo.Delete(role);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
