using Microsoft.Extensions.DependencyInjection;
using PostService.Application.Interfaces.Clients;
using PostService.Infrastructure.Grpc.Users.Services;

namespace PostService.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUserServiceClient, UserGrpcClientService>();

        return services;
    }
}
