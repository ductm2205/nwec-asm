using System;
using business.Commands.Questions;
using core.Exceptions;
using data.Infrastructures;
using models.Common;

namespace business.Handlers.Questions;

public class CreateHandler : BaseHandler<CreateCommand, bool>
{
    public CreateHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.QuizId) ?? throw new EntityNotFoundException();
        
        var newQues = new Question
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            QuestionType = request.QuestionType,
            IsActive = request.IsActive,
            QuizId = request.QuizId,
            Quiz = quiz,
            CreatedAt = DateTime.UtcNow,
        };

        _unitOfWork.QuestionRepo.Add(newQues);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
