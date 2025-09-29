using LessonModule.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Education.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected IActionResult FromResult<T>(Result<T> result)
    {
        if (result.IsSuccess && result.Value is not null)
            return Ok(result.Value);

        if (result.IsSuccess)
            return Ok(result.Value);

        return result.Error.Code switch
        {
            ErrorType.NotFound => NotFound(new { result.Error.Message }),
            ErrorType.InvalidArgument => BadRequest(new { result.Error.Message }),
            _ => BadRequest(new { result.Error.Message })
        };
    }

    protected IActionResult FromResult(Result result)
    {
        if (result.IsSuccess)
            return Ok(result);

        return result.Error.Code switch
        {
            ErrorType.NotFound => NotFound(new { result.Error.Message }),
            ErrorType.InvalidArgument => BadRequest(new { result.Error.Message }),
            _ => BadRequest(new { result.Error.Message })
        };
    }
}
