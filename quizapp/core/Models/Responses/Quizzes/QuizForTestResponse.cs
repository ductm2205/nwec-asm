namespace core.Models.Responses.Quizzes;

public class QuizForTestResponse : IResponse
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public string? Description { get; set; }

    public required Guid QuizCode { get; set; }

    public required DateTime StartTime { get; set; }

    public required int Duration { get; set; }

    public IEnumerable<QuestionResponse> Questions { get; set; } = [];

}
