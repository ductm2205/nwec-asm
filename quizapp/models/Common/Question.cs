using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Base;

namespace models.Common;

[Table("Questions", Schema = "common")]
public class Question : BaseEntity
{
    [Required]
    [StringLength(500)]
    [MinLength(5)]
    public required string Content { get; set; }

    [Required]
    public required QuestionType QuestionType { get; set; }


    public Guid QuizId { get; set; }
    public Quiz? Quiz { get; set; }

    public ICollection<Answer>? Answers { get; set; }

}
