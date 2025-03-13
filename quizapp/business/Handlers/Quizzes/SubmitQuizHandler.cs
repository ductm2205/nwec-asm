using business.Commands.Quizzes;
using core.Exceptions;
using data.Infrastructures;
using models.Relationship;

namespace business.Handlers.Quizzes;

public class SubmitQuizHandler : BaseHandler<SubmitQuizCommand, bool>
{
    public SubmitQuizHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override async Task<bool> HandleCommand(SubmitQuizCommand request, CancellationToken cancellationToken)
    {
        var userQuiz = _unitOfWork.UserQuizRepo.GetQuery(uq => uq.QuizId == request.QuizId && uq.UserId == request.UserId).FirstOrDefault() ?? throw new EntityNotFoundException();
        foreach (var userAnswer in request.UserAnswers)
        {
            var answer = new UserAnswer
            {
                UserQuizId = userQuiz.QuizCode,
                QuestionId = userAnswer.QuestionId,
                AnswerId = userAnswer.AnswerId,
            };

            _unitOfWork.UserAnswerRepo.Add(answer);
        }

        return await _unitOfWork.SaveChangesAsync(cancellationToken) > 0;
    }
}
