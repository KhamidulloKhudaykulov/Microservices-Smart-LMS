using CacheGateway.Logic.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace CacheGateway.Api.Controllers;

[ApiController]
[Route("api/cache/students")]
public class StudentCachingController : ControllerBase
{
    private readonly IStudentCacheService _studentCacheService;

    public StudentCachingController(IStudentCacheService studentCacheService)
    {
        _studentCacheService = studentCacheService;
    }

    [HttpGet("verify/{id}")]
    public async Task<IActionResult> VerifyExistStudent(Guid id)
    {
        var response = await _studentCacheService.VerifyExistStudentById(id);

        return Ok(response);
    }
}
