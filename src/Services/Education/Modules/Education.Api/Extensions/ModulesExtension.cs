using CourseModule.Application.Extensions;
using CourseModule.Infrastructure.Extensions;

namespace Education.Api.Extensions;

public static class ModulesExtension
{
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // CourseModule References
        services.AddCourseModuleApplication();
        services.AddCourseModuleInfrastructure(configuration);

        return services;
    }
}
