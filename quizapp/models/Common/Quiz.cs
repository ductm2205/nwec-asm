using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Base;
using models.Relationship;

namespace models.Common;

[Table("Quizzes", Schema = "common")]
public class Quiz : BaseEntity
{
    [Required]
    [StringLength(255)]
    [MinLength(5)]
    public required string Title { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(1, 3600, ErrorMessage = "Duration must be between 1 second and 1 hour.")]
    public required int Duration { get; set; }

    [StringLength(500)]
    public string? ThumbnailUrl { get; set; }

    public ICollection<Question>? Questions { get; set; } = [];
    public ICollection<UserQuiz>? UserQuizzes { get; set; } = [];
}
