using Identity.Application.Extensions;
using Identity.Infrastructure.Extensions;

namespace Identity.Api.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        services.AddApplication(configuration);
        services.AddInfrastructure(configuration);

        return services;
    }
}
