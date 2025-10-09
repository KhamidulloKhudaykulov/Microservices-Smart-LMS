using HomeworkModule.Application.Interfaces;
using HomeworkModule.Domain.Repositories;
using HomeworkModule.Infrastructure.Persistence;
using HomeworkModule.Infrastructure.Repositories;
using HomeworkModule.Infrastructure.Repositories.Decorators;
using HomeworkModule.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace HomeworkModule.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddHomeworkModuleInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<HomeworkDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationDbConnection"));
        });

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var options = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                AbortOnConnectFail = false,
                User = "admin",
                Password = "admin"
            };
            return ConnectionMultiplexer.Connect(options);
        });

        services.AddScoped<IHomeworkRepository, CachingHomeworkRepository>();

        services.AddScoped<IHomeworkUnitOfWork, HomeworkUnitOfWork>();
        services.AddScoped<IHomeworkServiceClient, HomeworkServiceClient>();

        services.AddScoped<HomeworkRepository>();
        services.AddScoped<IHomeworkRepository>(sp =>
        {
            var dbHomeworkRepository = sp.GetRequiredService<HomeworkRepository>();
            var redis = sp.GetRequiredService<IConnectionMultiplexer>();
            return new CachingHomeworkRepository(dbHomeworkRepository, redis);
        });

        return services;
    }
}
