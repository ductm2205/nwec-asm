using System;
using System.ComponentModel.DataAnnotations;
using models.Common;

namespace core.Models.Requests.Questions;

public class UpdateRequest : QuestionRequest
{
    [Required]
    public ICollection<core.Models.Requests.Answers.UpdateRequest> UpdateAnswers { get; set; } = [];
}
