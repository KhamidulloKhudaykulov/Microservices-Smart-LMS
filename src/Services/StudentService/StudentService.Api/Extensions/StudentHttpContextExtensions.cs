namespace StudentService.Api.Extensions;

public static class StudentHttpContextExtensions
{
    public static string? GetStudentId(this HttpContext context)
    {
        return context.Request.Headers["X-User-Id"].FirstOrDefault();
    }

    public static string[] GetStudentPermissions(this HttpContext context)
    {
        return context.Request.Headers["X-User-Permissions"]
            .FirstOrDefault()?
            .Split(',') ?? Array.Empty<string>();
    }
}
