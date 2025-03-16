using business.Commands.Roles;
using core.Exceptions;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Roles;

public class UpdateHandler : BaseHandler<UpdateCommand, bool>
{
    private readonly RoleManager<Role> _roleManager;
    public UpdateHandler(IUnitOfWork unitOfWork, RoleManager<Role> roleManager) : base(unitOfWork)
    {
        _roleManager = roleManager;
    }
    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        role.Description = request.Description;
        role.IsActive = request.IsActive;
        role.UpdatedAt = DateTime.UtcNow;

        var res = await _roleManager.UpdateAsync(role);
        
        return res.Succeeded;
    }
}
