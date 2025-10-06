using Microsoft.Extensions.DependencyInjection;

namespace CourseModel.Orchestration.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCourseModuleOrchestration(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
