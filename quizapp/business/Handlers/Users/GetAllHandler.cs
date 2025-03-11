using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;

namespace business.Handlers.Users;

public class GetAllHandler(IUnitOfWork unitOfWork) : BaseHandler<GetAllCommand<UserResponse>, PaginatedResult<UserResponse>>(unitOfWork)
{
    protected override async Task<PaginatedResult<UserResponse>> HandleCommand(GetAllCommand<UserResponse> request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepo.GetAllAsync();

        var userResponses = users.Select(user => new UserResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DisplayName = user.DisplayName,
            Email = user.Email!,
            UserName = user.UserName!,
            PhoneNumber = user.PhoneNumber!,
            IsActive = user.IsActive,
            DateOfBirth = user.DateOfBirth,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        }).ToList();

        return new PaginatedResult<UserResponse>(
            pageNumber: 1,
            pageSize: userResponses.Count,
            totalCount: userResponses.Count,
            items: userResponses
        );
    }
}
