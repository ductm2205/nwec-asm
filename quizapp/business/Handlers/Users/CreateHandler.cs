using business.Commands.Users;
using data.Infrastructures;
using models.Auth;

namespace business.Handlers.Users;

public class CreateHandler(IUnitOfWork unitOfWork) : BaseHandler<CreateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = request.UserName,
            Email = request.Email,
            PasswordHash = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _unitOfWork.UserRepo.Add(user);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
