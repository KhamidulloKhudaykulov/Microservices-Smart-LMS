using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Extensions;
using UserService.Application.UseCases.Users.Commands;

namespace UserService.Api.Controllers;

[ApiController]
[Route("api/users")]
public partial class UserController : ControllerBase
{
    private readonly ISender _sender;

    public UserController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [Authorize(Policy = "IdentityOnly")]
    public async Task<IActionResult> PostAsync(CreateUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery]GetUsersQuery query)
    {
        var permissions = HttpContext.GetUserPermissions();

        if (!permissions.Contains("user.get"))
            return Forbid();

        var result = await _sender.Send(query);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest();
    }
}
