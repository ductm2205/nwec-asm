using System.Threading.Tasks;
using business.Commands;
using business.Commands.Questions;
using business.Commands.Quizzes;
using core.Models;
using core.Models.Requests.Quizzes;
using core.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/quizzes")]
[ApiVersion("1.0")]
public class QuizzesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<QuizzesController> _logger;

    public QuizzesController(IMediator mediator, ILogger<QuizzesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(Guid id)
    {
        _logger.LogInformation("Getting quiz by id: {Id}", id);
        var query = new GetByIdCommand<QuizResponse> { Id = id };
        var res = await _mediator.Send(query);
        _logger.LogInformation("Finish, return quiz with id: {Id}", id);
        return Ok(res);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Getting all available quizzes");
        var query = new GetAllCommand<QuizResponse>();
        var res = await _mediator.Send(query);
        _logger.LogInformation("Return all available quizzes: {Quizzes}", res);
        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status201Created)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> CreateQuiz([FromBody] business.Commands.Quizzes.CreateCommand request)
    {
        _logger.LogInformation("Creating new quiz...");

        var res = await _mediator.Send(request);
        _logger.LogInformation("Finish creating new quiz");

        return Ok(res);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> UpdateQuiz(Guid id, [FromBody] business.Commands.Quizzes.UpdateCommand request)
    {
        _logger.LogInformation("Updating quiz with id: {Id}", id);

        _logger.LogInformation("Setting request id to: {Id}", id);
        request.Id = id;

        var res = await _mediator.Send(request);

        _logger.LogInformation("Finish updating quiz with id: {Id}", id);

        return Ok(res);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteQuiz(Guid id)
    {
        _logger.LogInformation("Deleting quiz with id: {Id}", id);
        var query = new QuizDeleteCommand { Id = id };
        var res = await _mediator.Send(query);
        _logger.LogInformation("Quiz with id: {Id} deleted", id);

        return Ok(res);
    }

    [HttpPost("add-question-to-quiz")]
    [ProducesResponseType<bool>(StatusCodes.Status201Created)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> AddQuestionToQuiz([FromBody] business.Commands.Questions.CreateCommand question)
    {
        _logger.LogInformation("Adding new question to quiz...");
        
        var res = await _mediator.Send(question);

        _logger.LogInformation("New question added");

        return Ok(res);
    }
}
