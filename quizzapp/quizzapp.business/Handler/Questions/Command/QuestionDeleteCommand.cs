using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;

namespace quizzapp.business.Handler.Questions.Command;

public class QuestionDeleteCommand : BaseCommand<bool>
{
    [Required(ErrorMessage = "Question ID is required")]
    public Guid Id { get; set; }
}