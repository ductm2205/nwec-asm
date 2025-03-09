using System;
using quizzapp.business.Handler.Base;

namespace quizzapp.business.Handler.Quizzes.Command;

public class QuizDeleteCommand : BaseCommand<bool>
{
    public Guid Id { get; set; }
}