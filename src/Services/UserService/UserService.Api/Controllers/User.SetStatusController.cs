using Microsoft.AspNetCore.Mvc;
using UserService.Application.UseCases.Users.Commands;

namespace UserService.Api.Controllers;

public partial class UserController : ControllerBase
{
    [HttpPut("activate")]
    public async Task<IActionResult> Activate(ActivateUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }

    [HttpPut("deactivate")]
    public async Task<IActionResult> Deactivate(DeactivateUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }

    [HttpPut("block")]
    public async Task<IActionResult> Block(BlockUserCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }
}
