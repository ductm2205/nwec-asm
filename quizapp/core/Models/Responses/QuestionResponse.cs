using models.Common;

namespace core.Models.Responses;

public class QuestionResponse : IResponse
{
    public Guid Id { get; set; }
    public required string Content { get; set; }

    public required QuestionType QuestionType { get; set; }

    public List<AnswerResponse> Answers { get; set; } = [];

    public Guid QuizId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
