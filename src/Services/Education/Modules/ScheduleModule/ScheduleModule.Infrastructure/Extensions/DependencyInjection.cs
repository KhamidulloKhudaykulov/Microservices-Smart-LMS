using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScheduleModule.Domain.Repositories;
using ScheduleModule.Infrastructure.Persistence;
using ScheduleModule.Infrastructure.Repositories;

namespace ScheduleModule.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddScheduleModuleInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ScheduleDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationDbConnection"));
        });

        services.AddScoped(typeof(IScheduleRepository<>), typeof(ScheduleRepository<>));
        services.AddScoped<IScheduleUnitOfWork, ScheduleUnitOfWork>();

        return services;
    }
}
