using Microsoft.Extensions.DependencyInjection;

namespace ScheduleModule.Orchestration.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddScheduleModuleOrchestration(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
