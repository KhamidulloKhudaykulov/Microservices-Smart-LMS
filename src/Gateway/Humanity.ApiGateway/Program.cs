using Humanity.ApiGateway.Extensions;
using Humanity.ApiGateway.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);
//builder.Services.AddAuthenticationExtension(builder.Configuration);
builder.Services.AddRateLimiterExtension();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseAuthenticationMiddleware();

app.UseRateLimiter();

//app.UseAuthentication();
//app.UseAuthorization();

app.UseClaimsForwardingMiddelware();

app.UseHttpsRedirection();

app.MapReverseProxy();

app.Run();
