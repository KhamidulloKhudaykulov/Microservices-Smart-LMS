using Identity.Application.Features.Authentication.CreateRole;
using Identity.Application.Features.Authentication.Login;
using Identity.Application.Features.Authentication.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthencticationController : ControllerBase
{
    private readonly ISender _sender;

    public AuthencticationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpGet]
    [Route("login")]
    public async Task<IActionResult> Login([FromQuery]LoginCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }

    [HttpPost]
    [Route("register-role")]
    public async Task<IActionResult> RegisterRole(CreateRoleCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess)
            return Ok(result);

        return BadRequest(result.Error.Message);
    }
}
