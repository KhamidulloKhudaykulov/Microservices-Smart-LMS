using CourseModule.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CourseModule.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddCourseModuleInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CourseDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultCourseDbConnection"));
        });

        return services;
    }
}
