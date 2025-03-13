using business.Commands.Questions;
using core.Exceptions;
using data.Infrastructures;
using Microsoft.EntityFrameworkCore;
using models.Common;

namespace business.Handlers.Questions;

public class UpdateHandler(IUnitOfWork unitOfWork) : BaseHandler<UpdateCommand, bool>(unitOfWork)
{
    protected override async Task<bool> HandleCommand(UpdateCommand request, CancellationToken cancellationToken)
    {
        var quiz = await _unitOfWork.QuizRepo.GetByIdAsync(request.QuizId) ?? throw new EntityNotFoundException();

        var ques = await _unitOfWork.QuestionRepo.GetByIdAsync(request.Id) ?? throw new EntityNotFoundException();

        ques.Content = request.Content ?? ques.Content;
        ques.QuestionType = request.QuestionType;
        ques.QuizId = quiz.Id;
        ques.IsActive = request.IsActive != ques.IsActive ? request.IsActive : ques.IsActive;
        ques.UpdatedAt = DateTime.UtcNow;

        var existingAnswers = await _unitOfWork.AnswerRepo.GetQuery(a => a.QuestionId == ques.Id).ToListAsync(cancellationToken: cancellationToken);

        // Process answers
        if (request.Answers != null && request.Answers.Any())
        {
            // Create a list to track processed answer IDs
            var processedAnswerIds = new List<Guid>();

            foreach (var answerRequest in request.Answers)
            {
                // Check if this is an existing answer (by content since new answers won't have IDs)
                var existingAnswer = existingAnswers.FirstOrDefault(a =>
                    a.Content == answerRequest.Content);

                if (existingAnswer != null)
                {
                    // Update existing answer
                    existingAnswer.IsCorrect = answerRequest.IsCorrect;
                    existingAnswer.IsActive = answerRequest.IsActive;
                    existingAnswer.UpdatedAt = DateTime.UtcNow;

                    _unitOfWork.AnswerRepo.Update(existingAnswer);
                    processedAnswerIds.Add(existingAnswer.Id);
                }
                else
                {
                    // Add new answer
                    var newAnswer = new Answer
                    {
                        Id = Guid.NewGuid(),
                        Content = answerRequest.Content,
                        IsCorrect = answerRequest.IsCorrect,
                        IsActive = answerRequest.IsActive,
                        QuestionId = ques.Id,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    _unitOfWork.AnswerRepo.Add(newAnswer);
                    processedAnswerIds.Add(newAnswer.Id);
                }
            }

            // Remove answers that weren't in the request
            foreach (var answer in existingAnswers)
            {
                if (!processedAnswerIds.Contains(answer.Id))
                {
                    _unitOfWork.AnswerRepo.Delete(answer);
                }
            }

        }
        _unitOfWork.QuestionRepo.Update(ques);
        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
