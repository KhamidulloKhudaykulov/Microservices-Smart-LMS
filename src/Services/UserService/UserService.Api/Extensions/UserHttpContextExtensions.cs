namespace UserService.Api.Extensions;

public static class UserHttpContextExtensions
{
    public static string? GetUserId(this HttpContext context)
    {
        return context.Request.Headers["X-User-Id"].FirstOrDefault();
    }

    public static string[] GetUserPermissions(this HttpContext context)
    {
        return context.Request.Headers["X-User-Permissions"]
            .FirstOrDefault()?
            .Split(',') ?? Array.Empty<string>();
    }
}
