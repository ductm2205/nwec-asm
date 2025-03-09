using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Roles.Command;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Roles.CommandHandler;

public class RoleGetByIdCommandHandler : BaseCommandHandler<RoleGetByIdCommand, Role>
{
    public RoleGetByIdCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<Role> HandleCommand(RoleGetByIdCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.RoleRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();
    }
}
