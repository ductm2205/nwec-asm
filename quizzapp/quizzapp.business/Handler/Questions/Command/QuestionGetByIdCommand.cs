using System;
using quizzapp.business.Handler.Base;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Questions.Command;

public class QuestionGetByIdCommand : BaseCommand<Question>
{
    public Guid Id { get; set; }
}