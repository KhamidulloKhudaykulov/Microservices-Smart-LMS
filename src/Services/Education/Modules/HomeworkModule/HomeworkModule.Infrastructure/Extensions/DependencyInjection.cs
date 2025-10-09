using HomeworkModule.Application.Interfaces;
using HomeworkModule.Domain.Repositories;
using HomeworkModule.Infrastructure.Persistence;
using HomeworkModule.Infrastructure.Repositories;
using HomeworkModule.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeworkModule.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddHomeworkModuleInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<HomeworkDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationDbConnection"));
        });

        services.AddScoped<IHomeworkRepository, HomeworkRepository>();
        services.AddScoped<IHomeworkUnitOfWork, HomeworkUnitOfWork>();
        services.AddScoped<IHomeworkServiceClient, HomeworkServiceClient>();

        return services;
    }
}
