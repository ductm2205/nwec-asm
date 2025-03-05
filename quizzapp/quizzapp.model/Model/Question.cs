using System.ComponentModel.DataAnnotations;
using quizzapp.model.Base;

namespace quizzapp.model.Model;

public class Question : BaseEntity
{
    [Required]
    [StringLength(500)]
    [MinLength(5)]
    public required string Content { get; set; }

    [Required]
    public required QuestionType QuestionType { get; set; }


    public Guid QuizzId { get; set; }

    public Quiz Quiz { get; set; }

    public ICollection<Answer>? Answers { get; set; }
}
