using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PaymentService.Domain.Repositories;
using PaymentService.Persistence.Repositories;
using StackExchange.Redis;
using StudentService.Infrastructure.Decorators.Repositories;

namespace PaymentService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection _services, 
        IConfiguration _configuration)
    {
        // Configure RedisSetting
        _services.Configure<RedisSetting>(
            _configuration.GetSection("redis"));

        // Configure RedisCaching
        _services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<RedisSetting>>().Value;
            var configuration = ConfigurationOptions.Parse(settings.Host, true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        // Inject Repositories
        _services.AddScoped<PaymentRepository>();
        _services.AddScoped<IPaymentRepository>(sp =>
        {
            var dbPaymentRepository = sp.GetRequiredService<PaymentRepository>();
            var redisSettings = sp.GetRequiredService<IOptions<RedisSetting>>().Value;

            if (redisSettings.Enabled)
            {
                var redis = sp.GetRequiredService<IConnectionMultiplexer>();
                return new CachingPaymentRepository(redis, dbPaymentRepository);
            }

            return dbPaymentRepository;
        });

        return _services;
    }
}
