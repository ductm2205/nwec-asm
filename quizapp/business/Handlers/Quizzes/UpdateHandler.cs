using business.Commands.Quizzes;
using core.Exceptions;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class UpdateHandler(IUnitOfWork unitOfWork) : BaseHandler<UpdateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        quiz.Title = request.Title ?? quiz.Title;
        quiz.Duration = request.Duration != quiz.Duration ? request.Duration : quiz.Duration;
        quiz.Description = request.Description ?? quiz.Description;
        quiz.ThumbnailUrl = request.ThumbnailUrl ?? quiz.ThumbnailUrl;
        quiz.IsActive = request.IsActive;
        quiz.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.QuizRepo.Update(quiz);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
