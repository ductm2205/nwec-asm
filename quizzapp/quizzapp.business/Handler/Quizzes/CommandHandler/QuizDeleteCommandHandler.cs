using System;
using System.Threading;
using System.Threading.Tasks;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Quizzes.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Quizzes.CommandHandler;

public class QuizDeleteCommandHandler : BaseCommandHandler<QuizDeleteCommand, bool>
{
    public QuizDeleteCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(QuizDeleteCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepository.GetByIdAsync(request.Id);
        if (quiz == null) return false;

        _unitOfWork.QuizRepository.Delete(quiz);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}