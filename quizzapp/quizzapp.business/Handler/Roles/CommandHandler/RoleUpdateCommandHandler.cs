using System;
using System.Threading;
using System.Threading.Tasks;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Roles.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Roles.CommandHandler;

public class RoleUpdateCommandHandler : BaseCommandHandler<RoleUpdateCommand, bool>
{
    public RoleUpdateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(RoleUpdateCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id);
        if (role == null) return false;

        role.Name = request.Name;
        role.Description = request.Description ?? role.Description;
        role.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.RoleRepository.Update(role);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}