using System;

namespace core.Models.Requests.Quizzes;

public class QuizCreateRequest : IRequest
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required int Duration { get; set; }

    public string? ThumbnailUrl { get; set; }
    
    public bool IsActive { get; set; } = true;
}
