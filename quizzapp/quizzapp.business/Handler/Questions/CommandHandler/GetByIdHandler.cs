using System;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Questions.Command;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Questions.CommandHandler;

public class GetByIdHandler : BaseCommandHandler<QuestionGetByIdCommand, Question>
{
    public GetByIdHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<Question> HandleCommand(QuestionGetByIdCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.QuestionRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();
    }
}
