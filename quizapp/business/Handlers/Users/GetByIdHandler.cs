using System;
using business.Commands;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Users;

public class GetByIdHandler(IUnitOfWork unitOfWork) : BaseHandler<GetByIdCommand<UserResponse>, UserResponse>(unitOfWork)
{
    protected override async Task<UserResponse> HandleCommand(GetByIdCommand<UserResponse> request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        var res = new UserResponse()
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email!,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth,
        };

        return res;
    }
}
