using Microsoft.Extensions.DependencyInjection;

namespace CourseModule.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCourseModuleApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
