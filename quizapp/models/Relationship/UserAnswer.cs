using System.ComponentModel.DataAnnotations.Schema;
using models.Base;

namespace models.Relationship;

[Table("UserAnswers", Schema = "common")]
public class UserAnswer : BaseEntity
{
    public Guid UserQuizId { get; set; }
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }
    public bool IsCorrect { get; set; } = false;
}