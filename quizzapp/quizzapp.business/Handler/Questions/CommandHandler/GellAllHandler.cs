using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Questions.Command;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Questions.CommandHandler;

public class GellAllHandler : BaseCommandHandler<QuestionGetAllCommand, IEnumerable<Question>>
{
    public GellAllHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<IEnumerable<Question>> HandleCommand(QuestionGetAllCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.QuestionRepository.GetAllAsync();
    }
}
