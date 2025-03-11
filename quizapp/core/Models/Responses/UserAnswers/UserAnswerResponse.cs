using System;

namespace core.Models.Responses.UserAnswers;

public class UserAnswerResponse : IResponse
{
    public Guid QuestionId { get; set; }
    public Guid AnswerId { get; set; }

}
