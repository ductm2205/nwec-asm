using System;
using business.Commands.Quizzes;
using core.Exceptions;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class UpdateHandler : BaseHandler<UpdateCommand, bool>
{
    public UpdateHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.QuizRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        role.Title = request.Title ?? role.Title;
        role.Duration = request.Duration != role.Duration ? request.Duration : role.Duration;
        role.Description = request.Description ?? role.Description;
        role.ThumbnailUrl = request.ThumbnailUrl ?? role.ThumbnailUrl;
        role.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.QuizRepo.Update(role);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
