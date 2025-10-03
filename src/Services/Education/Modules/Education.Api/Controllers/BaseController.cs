using LessonModule.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    private IActionResult MapError(Error error) => error.Code switch
    {
        ErrorType.NotFound => NotFound(new { error.Message }),
        ErrorType.InvalidArgument => BadRequest(new { error.Message }),
        _ => BadRequest(new { error.Message })
    };

    protected IActionResult FromResult<T>(Result<T> result)
        => result.IsSuccess ? Ok(result.Value) : MapError(result.Error);

    protected IActionResult FromResult(Result result)
        => result.IsSuccess ? Ok(result) : MapError(result.Error);
}
