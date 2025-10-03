using CourseModule.Application.UseCases.Courses.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers.Courses;

[Route("api/courses")]
public partial class CourseController
{
    [HttpPut("close/{courseId}")]
    public async Task<IActionResult> Close(Guid courseId)
    {
        var command = new CloseCourseCommand(courseId);
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return BadRequest(response.Error.Message);

        return FromResult(response);
    }

    [HttpPut("block/{courseId}")]
    public async Task<IActionResult> Block(Guid courseId)
    {
        var command = new BlockCourseCommand(courseId);
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return BadRequest(response.Error.Message);

        return FromResult(response);
    }
}
