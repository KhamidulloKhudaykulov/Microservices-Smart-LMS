using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TeacherService.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TeacherServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultStudentDbConnection"));
        });

        return services;
    }
}
