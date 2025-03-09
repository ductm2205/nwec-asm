using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Questions.Command;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;

namespace quizzapp.business.Handler.Questions.CommandHandler;

public class QuestionUpdateCommandHandler : BaseCommandHandler<QuestionUpdateCommand, bool>
{
    public QuestionUpdateCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(QuestionUpdateCommand request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        ques.Content = request.Content;
        ques.QuestionType = request.QuestionType;

        _unitOfWork.QuestionRepository.Update(ques);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}
