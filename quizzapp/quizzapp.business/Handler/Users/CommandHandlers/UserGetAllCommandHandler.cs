using System;
using Microsoft.EntityFrameworkCore;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Users.Commands;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Users.CommandHandlers;

public class UserGetAllCommandHandler : BaseCommandHandler<UserGetAllCommand, IEnumerable<User>>
{
    public UserGetAllCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<IEnumerable<User>> HandleCommand(UserGetAllCommand request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.UserRepository.GetQuery();

        if (!request.IncludeInactive)
        {
            query = query.Where(u => u.IsActive);
        }

        return await query.ToListAsync(cancellationToken: cancellationToken);
    }
}
