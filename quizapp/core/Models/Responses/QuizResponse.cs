using System;

namespace core.Models.Responses;

public class QuizResponse : IResponse
{
        public Guid Id { get; set; }
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required int Duration { get; set; }

    public string? ThumbnailUrl { get; set; }

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
