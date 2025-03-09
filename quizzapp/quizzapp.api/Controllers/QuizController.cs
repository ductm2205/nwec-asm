using MediatR;
using Microsoft.AspNetCore.Mvc;
using quizzapp.business.Handler.Quizzes.Command;

namespace quizzapp.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuizController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateQuiz([FromBody] QuizCreateCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateQuiz(Guid id, [FromBody] QuizUpdateCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("Quiz ID mismatch");
        }

        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteQuiz(Guid id)
    {
        var command = new QuizDeleteCommand { Id = id };
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetQuizById(Guid id)
    {
        var query = new QuizGetByIdCommand { Id = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllQuizzes()
    {
        var query = new QuizGetAllCommand();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}