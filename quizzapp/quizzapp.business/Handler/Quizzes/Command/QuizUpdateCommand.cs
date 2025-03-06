using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Quizzes.Command;

public class QuizUpdateCommand : BaseCommand<bool>
{
    [Required(ErrorMessage = "Quiz ID is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public required string Title { get; set; }

    [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
    public string? Description { get; set; }

    [Range(1, 300, ErrorMessage = "Duration must be between 1-300 minutes")]
    public int Duration { get; set; }

    [Url(ErrorMessage = "Invalid thumbnail URL format")]
    public string? ThumbnailUrl { get; set; }
}
