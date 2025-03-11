using System;
using core.Models.Responses.Quizzes;

namespace business.Commands.Quizzes;

public class TakeQuizCommand : BaseCommand<QuizForTestResponse>
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
}
