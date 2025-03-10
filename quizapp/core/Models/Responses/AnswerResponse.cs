namespace core.Models.Responses;

public class AnswerResponse : IResponse
{
    public Guid Id { get; set; }
    public required string Content { get; set; }

    public bool IsCorrect { get; set; } = false;

    public Guid QuestionId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
