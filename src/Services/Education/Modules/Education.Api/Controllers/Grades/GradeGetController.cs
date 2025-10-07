using GradeModule.Orchestration.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers.Grades;

public partial class GradeController
{
    [HttpGet("lesson/{lessonId}/{studentId}")]
    public async Task<IActionResult> GetByLessonId(Guid lessonId, Guid studentId)
    {
        var query = new GetGradesByLessonIdQuery(lessonId, studentId);
        var result = await _sender.Send(query);
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result);
    }

    [HttpGet("course/{lessonId}/{studentId}")]
    public async Task<IActionResult> GetByCourseId(Guid courseId, Guid studentId)
    {
        var query = new GetStudentGradesByCourseIdQuery(studentId, courseId);
        var result = await _sender.Send(query);
        if (result.IsSuccess)
            return FromResult(result);

        return BadRequest(result);
    }
}
