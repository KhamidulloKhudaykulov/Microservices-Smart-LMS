namespace Humanity.ApiGateway.Middlewares;

public class ClaimsForwardingMiddleware
{
    private readonly RequestDelegate _next;

    public ClaimsForwardingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.User.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.FindFirst("jti")?.Value;
            var permissions = string.Join(",", context.User.FindAll("permission").Select(c => c.Value));

            if (!string.IsNullOrEmpty(userId))
                context.Request.Headers["X-User-Id"] = userId;
            if (!string.IsNullOrEmpty(permissions))
                context.Request.Headers["X-User-Permissions"] = permissions;
        }

        await _next(context);
    }
}

public static class ClaimsForwardingMiddlewareExtensions
{
    public static IApplicationBuilder UseClaimsForwardingMiddelware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ClaimsForwardingMiddleware>();
    }
}
