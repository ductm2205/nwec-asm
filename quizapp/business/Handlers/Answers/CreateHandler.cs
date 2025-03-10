using business.Commands.Answers;
using core.Exceptions;
using data.Infrastructures;
using models.Common;

namespace business.Handlers.Answers;

public class CreateHandler(IUnitOfWork unitOfWork) : BaseHandler<CreateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var ques = await _unitOfWork.QuestionRepo.GetByIdAsync(request.QuestionId) ?? throw new EntityNotFoundException();

        var newQues = new Answer
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            IsCorrect = request.IsCorrect,
            QuestionId = ques.Id,
            IsActive = request.IsActive,
            CreatedAt = DateTime.UtcNow,
        };

        _unitOfWork.AnswerRepo.Add(newQues);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
