using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
        _services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse(_configuration.GetSection("redis:host").Value!, true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        _services.AddScoped<PaymentRepository>();
        _services.AddScoped<IPaymentRepository>(sp =>
        {
            var dbRepo = sp.GetRequiredService<PaymentRepository>();
            var redis = sp.GetRequiredService<IConnectionMultiplexer>();
            return new CachingPaymentRepository(redis, dbRepo);
        });

        return _services;
    }
}
