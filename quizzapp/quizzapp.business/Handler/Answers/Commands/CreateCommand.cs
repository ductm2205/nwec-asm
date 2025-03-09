using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Answers.Commands;

public class CreateCommand : BaseCommand<Answer>
{
    [Required]
    [StringLength(255)]
    [MinLength(5)]
    public required string Content { get; set; }

    public bool IsCorrect { get; set; } = false;

    public Guid QuestionId { get; set; }
}
