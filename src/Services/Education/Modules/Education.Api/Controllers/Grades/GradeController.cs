using GradeModule.Application.UseCases.Courses.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers.Grades;

[Route("api/education/grades")]
[Tags("2. Работа с оценками")]
public partial class GradeController : BaseController
{
    private readonly ISender _sender;

    public GradeController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> AssignLessonGrade(CreateLessonGradeCommand command)
    {
        var result = await _sender.Send(command);
        if (result.IsSuccess) 
            return FromResult(result);

        return BadRequest(result);
    }
}
