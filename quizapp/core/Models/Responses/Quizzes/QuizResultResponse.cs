namespace core.Models.Responses.Quizzes;

public class QuizResultResponse : IResponse
{
    public Guid QuizId { get; set; }
    public Guid UserId { get; set; }
    public int CorrectAnswers { get; set; }
    public int TotalQuestions { get; set; }
    public decimal Score { get; set; }
}
