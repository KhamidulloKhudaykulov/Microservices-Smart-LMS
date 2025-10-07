using Microsoft.Extensions.DependencyInjection;

namespace GradeModule.Orchestration.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddGradeModuleOrchestration(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
