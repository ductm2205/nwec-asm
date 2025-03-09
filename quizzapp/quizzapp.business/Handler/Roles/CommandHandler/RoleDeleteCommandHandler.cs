using System.Threading;
using System.Threading.Tasks;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Roles.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Roles.CommandHandler;

public class RoleDeleteCommandHandler : BaseCommandHandler<RoleDeleteCommand, bool>
{
    public RoleDeleteCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(RoleDeleteCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetByIdAsync(request.Id);
        if (role == null) return false;

        _unitOfWork.RoleRepository.Delete(role);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}