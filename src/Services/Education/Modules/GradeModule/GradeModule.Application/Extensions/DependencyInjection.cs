using Microsoft.Extensions.DependencyInjection;

namespace GradeModule.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddGradeModuleApplication(
        this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        });

        return services;
    }
}
