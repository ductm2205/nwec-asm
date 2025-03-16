using System.ComponentModel.DataAnnotations;
using business.Commands.Users;
using data.Infrastructures;
using Microsoft.AspNetCore.Identity;
using models.Auth;

namespace business.Handlers.Users;

public class DeleteHandler : BaseHandler<UserDeleteCommand, bool>
{
    private readonly UserManager<User> _userManager;

    public DeleteHandler(IUnitOfWork unitOfWork, UserManager<User> userManager) : base(unitOfWork)
    {
        _userManager = userManager;
    }

    protected override async Task<bool> HandleCommand(UserDeleteCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepo.GetByIdAsync(request.Id);
        if (user == null) return false;

        if (request.HardDelete)
        {
            _unitOfWork.UserRepo.Delete(user);
            return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
        }
        else
        {
            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedAt = DateTime.UtcNow;

            var res = await _userManager.UpdateAsync(user);

            return res.Succeeded;
        }
    }
}
