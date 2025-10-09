using Microsoft.Extensions.DependencyInjection;

namespace HomeworkModule.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddHomeworkModuleApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
