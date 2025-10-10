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
    public async Task<IActionResult> Create([FromQuery] CreateHomeworkCommand commmand)
    {
        var result = await _sender.Send(commmand);
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpPut("overdue/{courseId}/{id}")]
    public async Task<IActionResult> Overdue(Guid courseId, Guid id)
    {
        var result = await _sender.Send(new OverdueHomeworkCommand(courseId, id));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpPut("update/endtime/{courseId}/{id}/{time}")]
    public async Task<IActionResult> UpdateEndTime(Guid courseId, Guid id, DateTime time)
    {
        var result = await _sender.Send(
            new UpdateHomeworkEndTimeCommand(courseId, id, time));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpGet("lesson/{courseId}/{lessonId}")]
    public async Task<IActionResult> GetByLessonId(Guid courseId, Guid lessonId)
    {
        var result = await _sender.Send(new GetHomeworksByLessonIdQuery(courseId, lessonId));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetByCourseId(Guid courseId)
    {
        var result = await _sender.Send(new GetHomeworksByCourseIdQuery(courseId));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }

    [HttpGet("{courseId}/{homeworkId}")]
    public async Task<IActionResult> GetByCourseId(Guid courseId, Guid homeworkId)
    {
        var result = await _sender.Send(new GetHomeworkByIdQuery(courseId, homeworkId));
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result.Error);
    }
}
