using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentService.Application.UseCases.Students.Interfaces;
using StudentService.Infrastructure.Services.Users;

namespace StudentService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddGrpc();

        return services;
    }
}
