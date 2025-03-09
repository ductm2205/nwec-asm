using System;
using System.ComponentModel.DataAnnotations;
using quizzapp.business.Handler.Base;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Questions.Command;

public class QuestionUpdateCommand : BaseCommand<bool>
{
    [Required(ErrorMessage = "Question ID is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Question content is required")]
    [StringLength(1000, ErrorMessage = "Question content cannot exceed 1000 characters")]
    public required string Content { get; set; }


    public QuestionType QuestionType { get; set; }

}