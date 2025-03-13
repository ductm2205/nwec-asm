using business.Commands.Quizzes;
using core.Exceptions;
using core.Models.Responses.Quizzes;
using data.Infrastructures;

namespace business.Handlers.Quizzes;

public class GetQuizResultHandler : BaseHandler<GetQuizResultCommand, QuizResultResponse>
{
    public GetQuizResultHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    protected override Task<QuizResultResponse> HandleCommand(GetQuizResultCommand request, CancellationToken cancellationToken)
    {
        var userQuiz = _unitOfWork.UserQuizRepo.GetQuery(uq => uq.QuizId == request.QuizId && uq.UserId == request.UserId).First() ?? throw new EntityNotFoundException();

        var questions = userQuiz.Quiz?.Questions?.Count ?? 0;

        var correctAnswers = userQuiz.UserAnswers?.Count(ua => ua.IsCorrect) ?? 0;

        var score = (questions > 0) ? correctAnswers * 100 / questions : 0;

        var res = new QuizResultResponse
        {
            QuizId = userQuiz.QuizId,
            UserId = userQuiz.UserId,
            CorrectAnswers = correctAnswers,
            TotalQuestions = questions,
            Score = score
        };

        return Task.FromResult(res);
    }
}
