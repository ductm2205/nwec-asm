using quizzapp.model.Auth;
using quizzapp.model.Base;
using quizzapp.model.Model;

namespace quizzapp.model.Relation;

public class UserQuiz
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid QuizId { get; set; }
    public Quiz Quiz { get; set; }
    public string QuizCode { get; set; }
    public DateTime StartAt { get; set; }
    public DateTime FinishedAt { get; set; }
    public ICollection<UserAnswer> UserAnswers { get; set; } = [];
}
