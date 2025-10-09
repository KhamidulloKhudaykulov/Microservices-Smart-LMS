using HomeworkModule.Application.UseCases.Homeworks.Commands;
using HomeworkModule.Application.UseCases.Homeworks.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers.Homeworks;

[Route("api/homeworks")]
public class HomeworkController : BaseController
{
    private readonly ISender _sender;

    public HomeworkController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateHomeworkCommand commmand)
    {
        var result = await _sender.Send(commmand);
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpPut("overdue/{id}")]
    public async Task<IActionResult> Overdue(Guid id)
    {
        var result = await _sender.Send(new OverdueHomeworkCommand(id));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpPut("update/endtime/{id}/{time}")]
    public async Task<IActionResult> UpdateEndTime(Guid id, DateTime time)
    {
        var result = await _sender.Send(
            new UpdateHomeworkEndTimeCommand(id, time));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpGet("lesson/{lessonId}")]
    public async Task<IActionResult> GetByLessonId(Guid lessonId)
    {
        var result = await _sender.Send(new GetHomeworksByLessonIdQuery(lessonId));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }
}
