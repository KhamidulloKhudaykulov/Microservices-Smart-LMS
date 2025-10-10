using Microsoft.EntityFrameworkCore;
using ScheduleModule.Infrastructure.EfConfigurations;
using ScheduleModule.Infrastructure.Extensions;

namespace ScheduleModule.Infrastructure.Persistence;

public class ScheduleDbContext : DbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LessonScheduleConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
