using CourseModule.Infrastructure.Services.Courses;
using Integration.Infrastucture.Configurations;
using Integration.Infrastucture.Decorators;
using Integration.Infrastucture.Implementations;
using Integration.Logic.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Integration.Infrastucture.Extensions;
public static class DependencyInjection
{
    public static IServiceCollection AddIntegrationInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<RedisSettings>(configuration.GetSection("redis"));

        var redisSettings = configuration.GetSection("redis").Get<RedisSettings>();
        services.AddSingleton(redisSettings!);

        IConnectionMultiplexer? connectionMultiplexer = null;

        if (redisSettings!.IsEnabled && !string.IsNullOrWhiteSpace(redisSettings.Host))
        {
            try
            {
                connectionMultiplexer = ConnectionMultiplexer.Connect(redisSettings.Host!);
                services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);

                services.AddScoped<LessonIntegration>();
                services.AddScoped<ILessonIntegration>(sp =>
                {
                    var inner = sp.GetRequiredService<LessonIntegration>();
                    var redis = sp.GetRequiredService<IConnectionMultiplexer>();
                    return new LessonRedisIntegrationDecorator(inner, redis);
                });

                services.AddScoped<CourseIntegration>();
                services.AddScoped<ICourseIntegration>(sp =>
                {
                    var inner = sp.GetRequiredService<CourseIntegration>();
                    var redis = sp.GetRequiredService<IConnectionMultiplexer>();
                    return new CourseRedisIntegrationDecorator(inner, redis);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Redis connection failed: {ex.Message}");

                services.AddScoped<ILessonIntegration, LessonIntegration>();
                services.AddScoped<ICourseIntegration, CourseIntegration>();
            }
        }
        else
        {
            services.AddScoped<ILessonIntegration, LessonIntegration>();
            services.AddScoped<ICourseIntegration, CourseIntegration>();
        }

        return services;
    }
}
