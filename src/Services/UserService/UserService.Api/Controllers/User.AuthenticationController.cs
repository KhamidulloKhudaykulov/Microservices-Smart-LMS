//using Microsoft.AspNetCore.Mvc;
//using UserService.Application.UseCases.Users.Commands;

//namespace UserService.Api.Controllers;

//public partial class UserController : ControllerBase
//{
//    [HttpPost("auth")]
//    public async Task<IActionResult> Verify(VerifyUserCommand command)
//    {
//        var result = await _sender.Send(command);
//        if (result.IsFailure)
//            return BadRequest();

//        return Ok(result);
//    }
//}
