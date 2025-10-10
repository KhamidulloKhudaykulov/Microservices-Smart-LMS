using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleModule.Application.UseCases.LessonSchedules.Commands;

namespace Education.Api.Controllers.Schedules;

[Route("api/schedules")]
public class ScheduleController : BaseController
{
    private readonly ISender _sender;

    public ScheduleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("mark/absent/{lessonId}")]
    public async Task<IActionResult> MarkAbsent(Guid lessonId, List<Guid> studentIds)
    {
        var command = new MarkStudentsAbsentAtLessonCommand(lessonId, studentIds);
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return FromResult(response);

        return BadRequest(response);
    }

    [HttpPut("mark/present/{lessonId}")]
    public async Task<IActionResult> MarkPresent(Guid lessonId, List<Guid> studentIds)
    {
        var command = new MarkStudentsPresentAtLessonCommand(lessonId, studentIds);
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return FromResult(response);

        return BadRequest(response);
    }
}
