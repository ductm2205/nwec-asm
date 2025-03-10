namespace business.Commands.Answers;

public class CreateCommand : BaseCommand<bool>
{
    public required string Content { get; set; }

    public bool IsCorrect { get; set; } = false;

    public Guid QuestionId { get; set; }
    public bool IsActive { get; set; }
}
