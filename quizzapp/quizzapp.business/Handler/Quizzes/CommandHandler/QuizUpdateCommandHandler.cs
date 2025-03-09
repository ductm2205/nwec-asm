using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Quizzes.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Quizzes.CommandHandler;

public class QuizUpdateCommandHandler : BaseCommandHandler<QuizUpdateCommand, bool>
{
    public QuizUpdateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(QuizUpdateCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(request.Id);
        if (quiz == null) return false;

        quiz.Title = request.Title;
        quiz.Description = request.Description;
        quiz.Duration = request.Duration;
        quiz.ThumbnailUrl = request.ThumbnailUrl;
        quiz.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.QuizRepository.Update(quiz);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
