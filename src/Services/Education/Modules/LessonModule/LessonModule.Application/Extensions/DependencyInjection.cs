using Microsoft.Extensions.DependencyInjection;

namespace LessonModule.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddLessonModuleApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
