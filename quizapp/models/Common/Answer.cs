using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Base;

namespace models.Common;

[Table("Answers", Schema = "common")]
public class Answer : BaseEntity
{
    [Required]
    [StringLength(255)]
    [MinLength(5)]
    public required string Content { get; set; }

    public bool IsCorrect { get; set; } = false;

    public Guid QuestionId { get; set; }

    public Question Question { get; set; }
}
