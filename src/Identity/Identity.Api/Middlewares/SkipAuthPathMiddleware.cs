namespace Identity.Api.Middlewares;

public class SkipAuthPathMiddleware
{
    private readonly RequestDelegate _next;

    public SkipAuthPathMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/auth"))
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}

public static class SkipAuthPathMiddlewareExtensions
{
    public static IApplicationBuilder UseSkipAuthPath(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SkipAuthPathMiddleware>();
    }
}
