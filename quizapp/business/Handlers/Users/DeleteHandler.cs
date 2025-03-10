using business.Commands;
using data.Infrastructures;

namespace business.Handlers.Users;

public class DeleteHandler(IUnitOfWork unitOfWork) : BaseHandler<DeleteCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(DeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id);
        if (user == null) return false;

        _unitOfWork.UserRepo.Delete(user);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
