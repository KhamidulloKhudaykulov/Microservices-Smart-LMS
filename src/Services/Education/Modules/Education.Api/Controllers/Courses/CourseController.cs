using CourseModule.Application.UseCases.Courses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers.Courses;

[Route("api/education/courses")]
[Tags("1. Работа с курсами")]
public partial class CourseController : BaseController
{
    private readonly ISender _sender;

    public CourseController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCourse(CreateCourseCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return BadRequest(response.Error.Message);

        return FromResult(response);
    }

    [HttpPut("{courseId}/name/{name}")]
    public async Task<IActionResult> UpdateCourseName(Guid courseId, string name)
    {
        var command = new UpdateCourseNameCommand(courseId, name);
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return BadRequest(response.Error.Message);

        return FromResult(response);
    }

    [HttpPut("{courseId}/startsat/{startsAt}")]
    public async Task<IActionResult> UpdateCourseStartDate(Guid courseId, DateTime startsAt)
    {
        var command = new UpdateCourseStartDateCommand(courseId, startsAt);
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return BadRequest(response.Error.Message);

        return FromResult(response);
    }
}
