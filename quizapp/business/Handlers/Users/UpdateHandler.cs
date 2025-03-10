using business.Commands.Users;
using core.Exceptions;
using data.Infrastructures;

namespace business.Handlers.Users;

public class UpdateHandler(IUnitOfWork unitOfWork) : BaseHandler<UpdateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        user.UserName = request.UserName;
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.DateOfBirth = request.DateOfBirth;
        user.IsActive = request.IsActive;
        user.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.UserRepo.Update(user);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
