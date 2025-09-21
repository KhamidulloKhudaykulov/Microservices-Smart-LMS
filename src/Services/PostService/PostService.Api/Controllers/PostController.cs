using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostService.Application.UseCases.Posts.Commands;
using PostService.Application.UseCases.Posts.Queries;

namespace PostService.Api.Controllers;

[ApiController]
[Route("api/posts")]
public class PostController : ControllerBase
{
    private readonly ISender _sender;

    public PostController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]CreatePostCommand command)
    {
        var request = await _sender.Send(command);
        if (request.IsFailure)
        {
            return BadRequest(request.Error.Message);
        }

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]GetAllPostsQuery query)
    {
        var request = await _sender.Send(query);
        if (request.IsFailure)
        {
            return BadRequest(request.Error.Message);
        }

        return Ok(request);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(DeletePostCommand command)
    {
        var request = await _sender.Send(command);
        if (request.IsFailure)
        {
            return BadRequest(request.Error.Message);
        }
        return Ok();
    }
}
