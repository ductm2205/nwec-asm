using System;
using System.ComponentModel.DataAnnotations;
using core.Models.Requests.Answers;
using models.Common;

namespace core.Models.Requests.Questions;

public class QuestionRequest : IRequest
{
    [Required]
    public string Content { get; set; } = string.Empty;

    [Required]
    public QuestionType QuestionType { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public ICollection<AnswerRequest> Answers { get; set; } = [];

    public Guid QuizId { get; set; }
}
