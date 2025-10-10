using Microsoft.Extensions.DependencyInjection;

namespace ScheduleModule.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddScheduleModuleApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
