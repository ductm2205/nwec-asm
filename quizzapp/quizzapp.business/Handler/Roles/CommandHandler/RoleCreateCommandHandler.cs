using System;
using System.Threading;
using System.Threading.Tasks;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Roles.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Auth;

namespace quizzapp.business.Handler.Roles.CommandHandler
{
    public class RoleCreateCommandHandler : BaseCommandHandler<RoleCreateCommand, Role>
    {
        public RoleCreateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        protected override async Task<Role> HandleCommand(RoleCreateCommand request, CancellationToken cancellationToken)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description ?? "",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            await _unitOfWork.RoleRepository.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();
            return role;
        }
    }
}