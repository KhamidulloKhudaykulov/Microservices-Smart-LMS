﻿using LessonModule.Domain.Repositories;
using LessonModule.Infrastructure.Persistence;
using LessonModule.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LessonModule.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddLessonModuleInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<LessonDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EducationDbConnection"));
        });

        services.AddScoped<ILessonUnitOfWork, UnitOfWork>();
        services.AddScoped<ILessonRepository, LessonRepository>();

        return services;
    }
}
