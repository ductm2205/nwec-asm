using business.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller;

[ApiController]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    public readonly IMediator _mediator;
    public readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Login([FromBody] LoginCommand request)
    {
        _logger.LogInformation("Checking credentials...");

        var res = await _mediator.Send(request);

        return Ok(res);
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Register([FromBody] RegisterCommand request)
    {
        _logger.LogInformation("Registering new user...");

        var res = await _mediator.Send(request);

        return Ok(res);
    }
}
