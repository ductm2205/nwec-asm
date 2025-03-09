using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;

namespace quizzapp.business.Handler.Answers.Commands;

public class UpdateCommand : BaseCommand<bool>
{
    [Required]
    public required Guid Id { get; set; }
    [Required]
    [StringLength(255)]
    [MinLength(5)]
    public required string Content { get; set; }

    public bool IsCorrect { get; set; } = false;
}
