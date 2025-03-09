using System;
using quizzapp.business.Handler.Answers.Commands;
using quizzapp.business.Handler.Base;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Answers.Handlers;

public class CreateHandler : BaseCommandHandler<CreateCommand, Answer>
{
    public CreateHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<Answer> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepository.GetByIdAsync(request.QuestionId) ?? throw new EntityNotFoundException();

        Answer newA = new()
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            IsCorrect = request.IsCorrect,
            QuestionId = request.QuestionId
        };

        return await _unitOfWork.AnswerRepository.AddAsync(newA);
    }
}
