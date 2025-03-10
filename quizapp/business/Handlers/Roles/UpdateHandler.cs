using business.Commands.Roles;
using core.Exceptions;
using data.Infrastructures;

namespace business.Handlers.Roles;

public class UpdateHandler(IUnitOfWork unitOfWork) : BaseHandler<UpdateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        role.Description = request.Description;
        role.IsActive = request.IsActive;
        role.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.RoleRepo.Update(role);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
