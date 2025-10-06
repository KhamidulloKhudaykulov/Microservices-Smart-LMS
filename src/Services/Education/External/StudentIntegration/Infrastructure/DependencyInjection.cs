using Application.InterfaceBridges;
using Infrastructure.Grpc;
using Infrastructure.Grpc.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddStudentIntegrationInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddGrpcClient<StudentGrpcService.StudentGrpcServiceClient>(options =>
        {
            options.Address = new Uri(configuration["gRPC:student:host"]!);
        });


        services.AddScoped<IStudentServiceClient, StudentGrpcServiceClient>();

        return services;
    }

}
