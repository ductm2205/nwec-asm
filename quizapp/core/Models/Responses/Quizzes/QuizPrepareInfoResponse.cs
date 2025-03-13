namespace core.Models.Responses.Quizzes;

public class QuizPrepareInfoResponse : IResponse
{
    public Guid Id { get; set; }
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required int Duration { get; set; }

    public string? ThumbnailUrl { get; set; }

    public required string QuizCode { get; set; }

    public UserResponse? User { get; set; }
}
