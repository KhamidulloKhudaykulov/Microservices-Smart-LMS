using CourseModel.Orchestration.Queries;
using CourseModule.Application.UseCases.Courses.Commands;
using CourseModule.Application.UseCases.Courses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers.Courses;

public partial class CourseController
{
    [HttpPut("addstudent/{courseId}/{studentId}")]
    public async Task<IActionResult> Close(Guid courseId, Guid studentId)
    {
        var command = new AttachStudentCommand(courseId, studentId);
        var response = await _sender.Send(command);
        if (response.IsFailure)
            return BadRequest(response.Error.Message);

        return FromResult(response);
    }

    [HttpGet("students/{courseId}")]
    public async Task<IActionResult> GetCourseStudents(Guid courseId)
    {
        var query = new GetCourseWithStudentsQuery(courseId);
        var response = await _sender.Send(query);
        if (response.IsFailure)
            return BadRequest(response.Error.Message);

        return FromResult(response);
    }
}
