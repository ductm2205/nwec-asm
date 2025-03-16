using System;
using business.Commands;
using core.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/users")]
[ApiVersion("1.0")]
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
}
