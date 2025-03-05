namespace quizzapp.model.Relation;

public class UserAnswer
{
    public Guid Id { get; set; }
    public Guid UserQuizId { get; set; }
    public UserQuiz UserQuiz { get; set; }
    public Guid QuestionID { get; set; }
    public Guid AnswerId { get; set; }
    public bool? IsCorrect { get; set; }
}