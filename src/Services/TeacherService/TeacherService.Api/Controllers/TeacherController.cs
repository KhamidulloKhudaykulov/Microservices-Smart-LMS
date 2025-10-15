using MediatR;
using Microsoft.AspNetCore.Mvc;
using TeacherService.Application.UseCases.Teachers.Commands;
using TeacherService.Application.UseCases.Teachers.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TeacherService.Api.Controllers;

[ApiController]
[Route("api/teachers")]
public class TeacherController : ControllerBase
{
    private readonly ISender _sender;

    public TeacherController(ISender sender)
        => _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create(CreateTeacherCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsSuccess)
            return Ok(response.Value);

        return BadRequest(response.Error);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateTeacherCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsSuccess)
            return Ok(response.Value);

        return BadRequest(response.Error);
    }

    [HttpPut("deactivate")]
    public async Task<IActionResult> Deactivate(DeactivateTeacherCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsSuccess)
            return Ok(response.Value);

        return BadRequest(response.Error);
    }

    [HttpPut("activate")]
    public async Task<IActionResult> Activate(ActivateTeacherCommand command)
    {
        var response = await _sender.Send(command);
        if (response.IsSuccess)
            return Ok(response.Value);

        return BadRequest(response.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetTeacherByIdQuery(id);
        var response = await _sender.Send(query);
        if (response.IsSuccess)
            return Ok(response.Value);

        return BadRequest(response.Error);
    }

    [HttpGet("page:{page}/size:{pageSize}")]
    public async Task<IActionResult> GetAll(int page, int pageSize)
    {
        var query = new GetAllTeachersQuery(page, pageSize);
        var response = await _sender.Send(query);
        if (response.IsSuccess)
            return Ok(response.Value);

        return BadRequest(response.Error);
    }
}
