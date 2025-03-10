using System;
using System.ComponentModel.DataAnnotations.Schema;
using models.Auth;
using models.Base;
using models.Common;

namespace models.Relationship;

[Table("UserQuizzes", Schema = "common")]
public class UserQuiz : BaseItem
{
    public Guid QuizCode { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }
    public Guid QuizId { get; set; }

    public Quiz? Quiz { get; set; }

    public DateTime StartedAt { get; set; }
    public DateTime FinishedAt { get; set; }

    public UserAnswer? UserAnswer { get; set; }

}
