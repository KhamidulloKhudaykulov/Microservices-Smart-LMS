using CourseModule.Application.Interfaces;
using CourseModule.Domain.Repositories;
using CourseModule.Infrastructure.Persistence;
using CourseModule.Infrastructure.Repositories;
using CourseModule.Infrastructure.Services.Courses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Domain.Repositories;

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

        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICourseRepository, CourseRepository>();

        return services;
    }
}
