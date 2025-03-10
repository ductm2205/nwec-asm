using System;

namespace business.Commands.Quizzes;

public class UpdateCommand : CreateCommand, IHasIdCommand<bool>
{
    public Guid Id { get; set; }
}
