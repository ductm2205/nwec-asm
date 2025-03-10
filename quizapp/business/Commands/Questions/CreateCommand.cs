using models.Common;

namespace business.Commands.Questions;

public class CreateCommand : BaseCommand<bool>
{
    public required string Content { get; set; }

    public required QuestionType QuestionType { get; set; }

    public Guid QuizId { get; set; }
    public bool IsActive { get; set; }
}
