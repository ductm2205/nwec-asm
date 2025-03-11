using System;
using core.Models.Responses.UserAnswers;
using models.Relationship;

namespace business.Commands.Quizzes;

public class SubmitQuizCommand : BaseCommand<bool>
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }

    public List<UserAnswerResponse> UserAnswers { get; set; } = [];
}
