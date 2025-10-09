using GradeModule.Domain.Repositories;
using GradeModule.Infrastructure.Persistence;
using GradeModule.Infrastructure.Repositories;
using GradeModule.Infrastructure.Repsoitories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GradeModule.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddGradeModuleInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<GradeDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationDbConnection"));
        });

        services.AddScoped<IGradeRepository, GradeRepository>();
        services.AddScoped<IGradeHomeworkRepository, GradeHomeworkRepository>();
        services.AddScoped<IGradeUnitOfWork, UnitOfWork>();

        return services;
    }
}
