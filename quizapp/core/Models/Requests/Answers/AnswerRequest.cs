using System;
using System.ComponentModel.DataAnnotations;

namespace core.Models.Requests.Answers;

public class AnswerRequest : IRequest
{
    [Required]
    public string Content { get; set; } = string.Empty;

    public bool IsCorrect { get; set; } = false;

    public bool IsActive { get; set; } = true;
}
