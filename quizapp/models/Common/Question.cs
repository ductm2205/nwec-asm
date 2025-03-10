using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using models.Base;

namespace models.Common;

[Table("questions", Schema = "common")]
public class Question : BaseEntity
{
    [Required]
    [StringLength(500)]
    [MinLength(5)]
    public required string Content { get; set; }

    [Required]
    public required QuestionType QuestionType { get; set; }


    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }

    public ICollection<Answer> Answers { get; set; } = [];
}

public enum QuestionType
{
    [Display(Name = "MultipleChoice")]
    MultipleChoice,

    [Display(Name = "SingleChoice")]
    SingleChoice,

    [Display(Name = "TrueFalse")]
    TrueFalse,

    [Display(Name = "FillInTheBlanks")]
    FillInTheBlanks,

    [Display(Name = "ShortAnswer")]
    ShortAnswer,

    [Display(Name = "LongAnswer")]
    LongAnswer,
}