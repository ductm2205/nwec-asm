using business.Commands.Quizzes;
using core.Exceptions;
using data.Infrastructures;
using Microsoft.EntityFrameworkCore;
using models.Relationship;

namespace business.Handlers.Quizzes;

public class SubmitQuizHandler : BaseHandler<SubmitQuizCommand, bool>
{
    public SubmitQuizHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(SubmitQuizCommand request, CancellationToken cancellationToken)
    {
        // Get the user quiz
        var userQuiz = await _unitOfWork.UserQuizRepo.GetQuery(
            uq => uq.QuizId == request.QuizId && uq.UserId == request.UserId)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new EntityNotFoundException("User quiz not found");

        // Get the quiz with questions and answers
        var quiz = await _unitOfWork.QuizRepo.GetQuery(q => q.Id == request.QuizId)
            .Include(q => q.Questions!)
                .ThenInclude(q => q.Answers!)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? throw new EntityNotFoundException("Quiz not found");

        // Mark the quiz as finished
        userQuiz.FinishedAt = DateTime.UtcNow;

        // Process user answers
        foreach (var answer in request.UserAnswers)
        {
            // Find the question and selected answer
            var question = quiz.Questions?.FirstOrDefault(q => q.Id == answer.QuestionId);
            var selectedAnswer = question?.Answers?.FirstOrDefault(a => a.Id == answer.AnswerId);

            if (question != null && selectedAnswer != null)
            {
                // Check if the answer is correct
                var isCorrect = selectedAnswer.IsCorrect;

                // Create user answer record
                var userAnswer = new UserAnswer
                {
                    QuestionId = question.Id,
                    AnswerId = selectedAnswer.Id,
                    UserQuizId = userQuiz.Id,
                    IsCorrect = isCorrect
                };

                _unitOfWork.UserAnswerRepo.Add(userAnswer);
            }
        }

        // Save changes
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}
