using System;
using core.Models.Responses.Quizzes;

namespace business.Commands.Quizzes;

public class GetQuizResultCommand : BaseCommand<QuizResultResponse>
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }

}
