using System.Linq.Expressions;
using business.Commands.Quizzes;
using business.Handlers.Quizzes;
using data.Infrastructures;
using models.Common;
using models.Relationship;
using Moq;
using NUnit.Framework;

namespace test.business.handlers.quizzes;

[TestFixture]
public class GetQuizResultHandlerTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private GetQuizResultHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new GetQuizResultHandler(_mockUnitOfWork.Object);
    }

    [Test]
    public async Task HandleCommand_ShouldReturnQuizResultResponse_WhenUserQuizExists()
    {
        var quizId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var userQuiz = new UserQuiz
        {
            QuizCode = Guid.NewGuid(),
            QuizId = quizId,
            UserId = userId,
            Quiz = new Quiz
            {
                Title = "Quiz Title",
                Duration = 45,
                Questions =
                    [
                        new Question { Content = "Question 1", QuestionType = QuestionType.MultipleChoice },
                        new Question { Content = "Question 2", QuestionType = QuestionType.SingleChoice }
                    ]
            },
            UserAnswers =
                [
                    new UserAnswer { IsCorrect = true },
                    new UserAnswer { IsCorrect = false }
                ]
        };

        _mockUnitOfWork.Setup(uow => uow.UserQuizRepo.GetQuery(It.IsAny<Expression<Func<UserQuiz, bool>>>()))
            .Returns(new List<UserQuiz> { userQuiz }.AsQueryable());

        var command = new GetQuizResultCommand { QuizId = quizId, UserId = userId };

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.QuizId, Is.EqualTo(quizId));
            Assert.That(result.UserId, Is.EqualTo(userId));
            Assert.That(result.CorrectAnswers, Is.EqualTo(1));
            Assert.That(result.TotalQuestions, Is.EqualTo(2));
            Assert.That(result.Score, Is.EqualTo(50));
        });
    }
}
