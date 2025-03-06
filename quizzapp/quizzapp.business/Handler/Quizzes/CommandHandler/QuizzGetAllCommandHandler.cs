using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Quizzes.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Quizzes.CommandHandler;

public class QuizzGetAllCommandHandler : BaseCommandHandler<QuizGetAllCommand, IEnumerable<Quiz>>
{
    public QuizzGetAllCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<IEnumerable<Quiz>> HandleCommand(QuizGetAllCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.QuizRepository.GetAllAsync();
    }
}
