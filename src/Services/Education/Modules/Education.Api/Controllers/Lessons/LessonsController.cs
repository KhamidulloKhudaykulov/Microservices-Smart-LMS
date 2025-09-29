using LessonModule.Application.UseCases.Lessons.Commands;
using LessonModule.Application.UseCases.Lessons.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers.Lessons;

[Route("api/lessons")]
public class LessonsController : BaseController
{
    private readonly ISender _sender;

    public LessonsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateLesson(CreateLessonCommand command)
    {
        var result = await _sender.Send(command);
        return FromResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetLessonQuery(id);
        var result = await _sender.Send(query);
        return FromResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetLessonsByCourseQuery query)
    {
        var result = await _sender.Send(query);
        return FromResult(result);
    }
}
