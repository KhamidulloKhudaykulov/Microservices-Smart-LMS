using Identity.Application.Clients;
using Identity.Application.Common;
using Identity.Application.Interfaces;
using Identity.Domain.Repositories;
using Identity.Infrastructure.Clients;
using Identity.Infrastructure.Persistence;
using Identity.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Default"));
        });

        services.AddHttpClient("UserServiceClient", client =>
        {
            var baseUrl = configuration["ExternalClients:UserServiceHost"];
            client.BaseAddress = new Uri(baseUrl!);
        });

        services.AddScoped<IUserServiceClient, UserServiceClient>(sp =>
        {
            var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
            var jwtService = sp.GetRequiredService<JwtService>();
            return new UserServiceClient(httpClientFactory.CreateClient("UserServiceClient"), jwtService);
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<ITokenService, JwtService>();

        services.AddSingleton<JwtService>();

        return services;
    }
}
