using business.Commands.Roles;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Roles;

public class CreateHandler : BaseHandler<CreateCommand, bool>
{
    private readonly RoleManager<Role> _roleManager;
    public CreateHandler(IUnitOfWork unitOfWork, RoleManager<Role> roleManager) : base(unitOfWork)
    {
        _roleManager = roleManager;
    }

    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.RoleRepo.GetAllAsync();

        var newR = new Role
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        if (roles.Any(r => r.Name == newR.Name))
        {
            return false;
        }

        var res = await _roleManager.CreateAsync(newR);

        return res.Succeeded;
    }
}
