using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentService.Application.Interfaces.DomainEvents;
using StudentService.Application.Interfaces.Redis;
using StudentService.Application.UseCases.Students.Interfaces;
using StudentService.Infrastructure.DomainEvents;
using StudentService.Infrastructure.Services.Users;
using StudentService.Infrastructure.Utilities.Redis;

namespace StudentService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

        var redisConnection = configuration.GetSection("Redis:ConnectionString").Value;

        services.AddScoped<IRedisCacheService>(sp =>
            new RedisCacheService(redisConnection));

        services.AddGrpc();

        return services;
    }
}
