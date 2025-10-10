using CacheGateway.Infrastructure.Grpc.Client;
using CacheGateway.Infrastructure.Implementations;
using CacheGateway.Logic.Abstractions;
using Infrastructure.Grpc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CacheGateway.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {

        //var redisConnection = configuration.GetSection("Redis:ConnectionString").Value;

        services.AddGrpcClient<StudentGrpcService.StudentGrpcServiceClient>(options =>
        {
            options.Address = new Uri(configuration["gRPC:student:host"]);
        });

        services.AddScoped<StudentGrpcServiceClient>();
        services.AddScoped<IStudentCacheService, StudentCacheService>();

        return services;
    }
}
