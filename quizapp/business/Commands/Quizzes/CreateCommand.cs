using System;

namespace business.Commands.Quizzes;

public class CreateCommand : BaseCommand<bool>
{
    public required string Title { get; set; }

    public string? Description { get; set; }

    public required int Duration { get; set; }

    public string? ThumbnailUrl { get; set; }
}
