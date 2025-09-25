namespace Humanity.ApiGateway.Middlewares;

public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string[] _publicEndpoints;

    public AuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _publicEndpoints = configuration
            .GetSection("PublicEndpoints")
            .Get<string[]>() 
            ?? Array.Empty<string>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        if (_publicEndpoints.Any(ep => path!.StartsWith(ep)))
        {
            await _next(context);
            return;
        }

        var token = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Authorization token is missing or invalid");
            return;
        }

        context.Items["UserToken"] = token.Substring("Bearer ".Length).Trim();

        await _next(context);
    }

}

public static class AuthenticationMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthenticationMiddleware>();
    }
}
