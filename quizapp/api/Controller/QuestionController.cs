using System.Net;
using System.Threading.Tasks;
using business.Commands;
using business.Commands.Questions;
using core.Models;
using core.Models.Requests;
using core.Models.Responses;
using data.Infrastructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using models.Common;

namespace api.Controller;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/questions")]
[ApiVersion("1.0")]
public class QuestionController(IMediator mediator, ILogger<QuestionController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    private readonly ILogger<QuestionController> _logger = logger;

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<QuestionResponse>> GetById(Guid id)
    {
        _logger.LogInformation("Searching for question with Id: {Id}", id);
        var query = new GetByIdCommand<QuestionResponse> { Id = id };
        var res = await _mediator.Send(query);
        _logger.LogInformation("Search result: {Res}", res.ToString());
        return Ok(res);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PaginatedResult<QuestionResponse>>> GetAll()
    {
        _logger.LogInformation("Getting all questions");
        var query = new GetAllCommand<QuestionResponse>();

        var res = await _mediator.Send(query);
        _logger.LogInformation("Done fetching!");

        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateNewQuestion([FromBody] QuestionRequest request)
    {
        _logger.LogInformation("Creating new question");

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var query = new CreateCommand
        {
            Content = request.Content,
            QuestionType = request.QuestionType,
            IsActive = request.IsActive,
            QuizId = request.QuizId,
            Answers = [.. request.Answers.Select(
                a => new Answer
                {
                    Content = a.Content,
                    IsCorrect = a.IsCorrect,
                    IsActive = a.IsActive,
                }
            )],
        };

        var res = await _mediator.Send(query);

        if (!res)
        {
            _logger.LogError("Failed to create");
        }
        else
        {
            _logger.LogInformation("Created Successfully!");
        }

        return res ? StatusCode(StatusCodes.Status201Created, true) : BadRequest(false);
    }
}
