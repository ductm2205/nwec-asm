using System.ComponentModel.DataAnnotations;

namespace core.Models.Requests.Questions;

public class UpdateRequest : QuestionRequest
{
    [Required]
    public ICollection<core.Models.Requests.Answers.UpdateRequest> UpdateAnswers { get; set; } = [];
}
