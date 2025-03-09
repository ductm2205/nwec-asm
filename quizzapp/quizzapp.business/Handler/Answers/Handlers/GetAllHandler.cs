using System;
using quizzapp.business.Handler.Answers.Commands;
using quizzapp.business.Handler.Base;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Answers.Handlers;

public class GetAllHandler : BaseCommandHandler<GetAllCommand, IEnumerable<Answer>>
{
    public GetAllHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<IEnumerable<Answer>> HandleCommand(GetAllCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AnswerRepository.GetAllAsync();
    }
}
