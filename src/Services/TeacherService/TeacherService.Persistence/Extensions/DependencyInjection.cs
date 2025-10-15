using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Domain.Repositories;
using TeacherService.Domain.Repositories;
using TeacherService.Persistence.Repositories;

namespace TeacherService.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<TeacherServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultTeacherDbConnection"));
        });

        services.AddScoped<ITecherRepository, TeacherRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
