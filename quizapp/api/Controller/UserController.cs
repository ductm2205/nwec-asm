using business.Commands;
using business.Commands.Users;
using core.Models;
using core.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
[Authorize(Roles = "System Administrator, Administrator")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(IMediator mediator, ILogger<UserController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        _logger.LogInformation("Getting user with ID: {Id}", id);

        var command = new GetByIdCommand<UserResponse> { Id = id };
        var user = await _mediator.Send(command);

        if (user == null)
        {
            _logger.LogWarning("User with ID {Id} not found", id);
            return NotFound();
        }

        _logger.LogInformation("Successfully retrieved user with ID: {Id}", id);
        return Ok(user);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<PaginatedResult<UserResponse>>> GetAll()
    {
        _logger.LogInformation("Getting all users");
        var query = new GetAllCommand<UserResponse>();

        var res = await _mediator.Send(query);
        _logger.LogInformation("Done fetching!");

        return Ok(res);
    }

    [HttpPost]
    [ProducesResponseType<bool>(StatusCodes.Status201Created)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> CreateUser([FromBody] CreateCommand request)
    {
        _logger.LogInformation("Adding new user...");
        var res = await _mediator.Send(request);

        _logger.LogInformation("User created");
        return Ok(res);
    }

    [HttpPut("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UpdateCommand request)
    {
        if (id != request.Id)
        {
            _logger.LogWarning("ID mismatch: Path ID {PathId} doesn't match body ID {BodyId}", id, request.Id);
            return BadRequest("ID in the path must match ID in the request body");
        }

        _logger.LogInformation("Updating user with id: {Id}", id);

        var res = await _mediator.Send(request);

        _logger.LogInformation("User updated");
        return Ok(res);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        _logger.LogInformation("Deleting user with id: {Id}", id);
        var query = new UserDeleteCommand { Id = id };
        var res = await _mediator.Send(query);
        _logger.LogInformation("user with id: {Id} deleted", id);

        return Ok(res);
    }

    [HttpPost("change-password")]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    [ProducesResponseType<bool>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<bool>(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordCommand request)
    {

        _logger.LogInformation("Changing password for user with id: {Id}", request.Id);
        var res = await _mediator.Send(request);

        if (res)
        {
            _logger.LogInformation("Password updated for user with id: {Id}", request.Id);
        }

        return Ok(res);
    }
}
