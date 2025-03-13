using System.Threading.Tasks;
using business.Commands;
using core.Models;
using core.Models.Responses;
using data.Infrastructures;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<QuestionResponse>> GetById(Guid id)
    {
        _logger.LogInformation("Searching for question with Id: {Id}", id);
        var query = new GetByIdCommand<QuestionResponse> { Id = id };
        var res = await _mediator.Send(query);
        _logger.LogInformation("Search result: {Res}", res.ToString());
        return Ok(res);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<QuestionResponse>>> GetAll()
    {
        _logger.LogInformation("Getting all questions");
        var query = new GetAllCommand<QuestionResponse>();

        var res = await _mediator.Send(query);
        _logger.LogInformation("Done fetching!");

        return Ok(res);
    }
}
