using Infrastructure.Grpc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentIntegration.Application.InterfaceBridges;
using StudentIntegration.Infrastructure.Grpc.Client;

namespace StudentIntegration.Infrastructure;

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
