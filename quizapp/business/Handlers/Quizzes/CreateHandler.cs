using System;
using business.Commands.Quizzes;
using data.Infrastructures;
using models.Common;

namespace business.Handlers.Quizzes;

public class CreateHandler : BaseHandler<CreateCommand, bool>
{
    public CreateHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var newR = new Quiz
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration,
            ThumbnailUrl = request.ThumbnailUrl,
            CreatedAt = DateTime.UtcNow,
        };

        _unitOfWork.QuizRepo.Add(newR);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
