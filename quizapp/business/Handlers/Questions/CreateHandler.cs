using business.Commands.Questions;
using core.Exceptions;
using data.Infrastructures;
using models.Common;

namespace business.Handlers.Questions;

public class CreateHandler(IUnitOfWork unitOfWork) : BaseHandler<CreateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(CreateCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.QuizId) ?? throw new EntityNotFoundException();

        var newQues = new Question
        {
            Id = Guid.NewGuid(),
            Content = request.Content,
            QuestionType = request.QuestionType,
            IsActive = request.IsActive,
            Quiz = quiz,
            QuizId = quiz.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Answers = []
        };

        if (request.Answers != null && request.Answers.Count != 0)
        {
            foreach (var answer in request.Answers)
            {
                newQues.Answers.Add(new Answer
                {
                    Id = Guid.NewGuid(),
                    Content = answer.Content,
                    IsCorrect = answer.IsCorrect,
                    IsActive = answer.IsActive,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    QuestionId = newQues.Id
                });
            }
        }

        _unitOfWork.QuestionRepo.Add(newQues);

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
