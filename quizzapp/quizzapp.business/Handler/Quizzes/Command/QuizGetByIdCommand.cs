using System;
using quizzapp.business.Handler.Base;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Quizzes.Command;

public class QuizGetByIdCommand : BaseCommand<Quiz>
{
    public Guid Id { get; set; }
}
