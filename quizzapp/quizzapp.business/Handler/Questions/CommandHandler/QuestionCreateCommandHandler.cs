using System;
using System.Threading;
using System.Threading.Tasks;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Questions.Command;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Questions.CommandHandler;

public class QuestionCreateCommandHandler : BaseCommandHandler<QuestionCreateCommand, Question>
{
    public QuestionCreateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<Question> HandleCommand(QuestionCreateCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(request.QuizId)
            ?? throw new EntityNotFoundException("Quiz not found");

        var question = new Question
        {
            Id = Guid.NewGuid(),
            QuizId = request.QuizId,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            QuestionType = request.QuestionType,
        };

        await _unitOfWork.QuestionRepository.AddAsync(question);
        await _unitOfWork.SaveChangesAsync();
        return question;
    }
}