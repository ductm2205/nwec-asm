using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Quizzes.Command;
using quizzapp.data.Infrastructure;

namespace quizzapp.business.Handler.Quizzes.CommandHandler;

public class QuizCreateCommandHandler : BaseCommandHandler<QuizCreateCommand, model.Model.Quiz>
{
    public QuizCreateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<model.Model.Quiz> HandleCommand(QuizCreateCommand request, CancellationToken cancellationToken)
    {
        var newQuizId = Guid.NewGuid();
        var newQ = new model.Model.Quiz
        {
            Id = newQuizId,
            Title = request.Title,
            Description = request.Description,
            Duration = request.Duration,
            ThumbnailUrl = request.ThumbnailUrl
        };

        await _unitOfWork.QuizRepository.AddAsync(newQ);
        await _unitOfWork.SaveChangesAsync();
        return newQ;
    }
}
