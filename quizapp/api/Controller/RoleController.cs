using business.Commands;
using business.Commands.Roles;
using core.Models;
using core.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;


[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/roles")]
[ApiVersion("1.0")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<RoleController> _logger;

    public RoleController(IMediator mediator, ILogger<RoleController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> GetRoleById(Guid id)
    {
        _logger.LogInformation("Searching for role with id: {Id}", id);

        var req = new GetByIdCommand<RoleResponse> { Id = id };

        var res = await _mediator.Send(req);

        _logger.LogInformation("Role found with id: {Id}", id);

        return Ok(res);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PaginatedResult<RoleResponse>>> GetAll()
    {
        _logger.LogInformation("Getting all roles");
        var query = new GetAllCommand<RoleResponse>();

        var res = await _mediator.Send(query);
        _logger.LogInformation("Done fetching!");

        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status201Created)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateRole([FromBody] CreateCommand request)
    {
        _logger.LogInformation("Adding new role...");
        var res = await _mediator.Send(request);

        if (res)
        {
            _logger.LogInformation("Role created");
        }
        return Ok(res);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateRole(Guid id, [FromBody] UpdateCommand request)
    {
        if (id != request.Id)
        {
            _logger.LogWarning("ID mismatch: Path ID {PathId} doesn't match body ID {BodyId}", id, request.Id);
            return BadRequest("ID in the path must match ID in the request body");
        }

        _logger.LogInformation("Updating role with id: {Id}", id);

        var res = await _mediator.Send(request);

        if (res)
        {
            _logger.LogInformation("Role updated");
        }
        return Ok(res);
    }
}
