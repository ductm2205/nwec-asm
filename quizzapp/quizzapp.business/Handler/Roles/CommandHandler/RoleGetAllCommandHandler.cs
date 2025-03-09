using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Roles.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Roles.CommandHandler;

public class RoleGetAllCommandHandler : BaseCommandHandler<RoleGetAllCommand, IEnumerable<Role>>
{
    public RoleGetAllCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<IEnumerable<Role>> HandleCommand(RoleGetAllCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.RoleRepository.GetAllAsync();
    }
}