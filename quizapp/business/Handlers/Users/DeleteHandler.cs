using business.Commands;
using business.Commands.Users;
using data.Infrastructures;

namespace business.Handlers.Users;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<UserDeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id);
        if (user == null) return false;

        _unitOfWork.UserRepo.Delete(user);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
