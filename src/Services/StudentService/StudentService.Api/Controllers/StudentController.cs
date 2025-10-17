using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentService.Api.Extensions;
using StudentService.Application.UseCases.Students.Commands;
using StudentService.Application.UseCases.Students.Queries;

namespace StudentService.Api.Controllers;

[ApiController]
[Route("api/students")]
public partial class StudentController : ControllerBase
{
    private readonly ISender _sender;

    public StudentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateStudentCommand command)
    {
        var result = await _sender.Send(command);
        return result.IsSuccess
            ? Ok()
            : BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] GetAllStudentsQuery query)
    {
        var permissions = HttpContext.GetStudentPermissions();

        //if (!permissions.Contains("student.get"))
        //    return Forbid();

        var result = await _sender.Send(query);
        return result.IsSuccess
            ? Ok(result.Value)
            : BadRequest();
    }
}