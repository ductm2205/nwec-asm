using System;
using quizzapp.business.Handler.Base;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Answers.Commands;

public class GetByIdCommand : BaseCommand<Answer>
{
    public Guid Id { get; set; }
}
