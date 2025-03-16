using business.Commands.Roles;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Roles;

public class DeleteHandler : BaseHandler<RoleDeleteCommand, bool>
{
    private readonly RoleManager<Role> _roleManager;
    public DeleteHandler(IUnitOfWork unitOfWork, RoleManager<Role> roleManager) : base(unitOfWork)
    {
        _roleManager = roleManager;
    }

    protected override async Task<bool> HandleCommand(RoleDeleteCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepo.GetByIdAsync(request.Id);
        if (role == null) return false;

        if (request.HardDelete)
        {
            var res = await _roleManager.DeleteAsync(role);
            return res.Succeeded;
        }
        else
        {
            role.IsActive = false;
            role.IsDeleted = true;
            role.DeletedAt = DateTime.UtcNow;

            var res = await _roleManager.UpdateAsync(role);
            return res.Succeeded;
        }
    }
}
