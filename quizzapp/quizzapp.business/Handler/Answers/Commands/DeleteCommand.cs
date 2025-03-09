using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Answers.Commands;

public class DeleteCommand : BaseCommand<bool>
{
    [Required]
    public required Guid Id { get; set; }
}
