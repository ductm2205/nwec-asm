using System;
using Microsoft.EntityFrameworkCore;
using quizzapp.business.Handler.Base;
using quizzapp.business.Handler.Quizzes.Command;
using quizzapp.core.Exceptions;
using quizzapp.data.Infrastructure;
using quizzapp.model.Model;

namespace quizzapp.business.Handler.Quizzes.CommandHandler;

public class QuizGetByIdCommandHandler : BaseCommandHandler<QuizGetByIdCommand, Quiz>
{
    public QuizGetByIdCommandHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<Quiz> HandleCommand(QuizGetByIdCommand request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.QuizRepository.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();
    }
}
