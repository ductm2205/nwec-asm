using business.Commands.Quizzes;
using data.Infrastructures;
using models.Common;

namespace business.Handlers.Quizzes;

public class CreateHandler(IUnitOfWork unitOfWork) : BaseHandler<CreateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var newQuiz = new Quiz
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration,
            ThumbnailUrl = request.ThumbnailUrl,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
        };

        _unitOfWork.QuizRepo.Add(newQuiz);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
