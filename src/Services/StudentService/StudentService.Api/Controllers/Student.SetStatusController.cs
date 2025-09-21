using Microsoft.AspNetCore.Mvc;
using StudentService.Application.UseCases.Students.Commands;

namespace StudentService.Api.Controllers;

public partial class StudentController : ControllerBase
{
    [HttpPut("activate")]
    public async Task<IActionResult> Activate(ActivateStudentCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }

    [HttpPut("deactivate")]
    public async Task<IActionResult> Deactivate(DeactivateStudentCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }

    [HttpPut("block")]
    public async Task<IActionResult> Block(BlockStudentCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }
}
