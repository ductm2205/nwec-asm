using System.Net;
using System.Threading.Tasks;
using business.Commands;
using business.Commands.Questions;
using core.Exceptions;
using core.Models;
using core.Models.Requests.Questions;
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

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateQuestionWithAnswer(Guid id, [FromBody] QuestionRequest questionEditViewModel)
    {
        _logger.LogInformation("Updating question with Id: {Id}", id);

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var command = new UpdateCommand
        {
            Id = id,
            Content = questionEditViewModel.Content,
            QuestionType = questionEditViewModel.QuestionType,
            IsActive = questionEditViewModel.IsActive,
            QuizId = questionEditViewModel.QuizId,
            Answers = [.. questionEditViewModel.Answers.Select(
                a => new Answer
                {
                    Id = a.Id,
                    Content = a.Content,
                    IsCorrect = a.IsCorrect,
                    IsActive = a.IsActive,
                }
            )],
        };

        try
        {
            var result = await _mediator.Send(command);

            if (!result)
            {
                _logger.LogError("Failed to update question with Id: {Id}", id);
                return BadRequest(false);
            }

            _logger.LogInformation("Question updated successfully with ID: {Id}", id);
            return Ok(true);
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogError(ex, "Question with Id: {Id} not found", id);
            return NotFound(false);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteQuestion(Guid id)
    {
        _logger.LogInformation("Deleting question with Id: {Id}", id);
        var query = new QuestionDeleteCommand { Id = id };

        try
        {
            var result = await _mediator.Send(query);

            if (!result)
            {
                _logger.LogError("Failed to delete question with Id: {Id}", id);
                return BadRequest(false);
            }

            _logger.LogInformation("Question deleted successfully with ID: {Id}", id);
            return Ok(true);
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogError(ex, "Question with Id: {Id} not found", id);
            return NotFound(false);
        }
    }
}
