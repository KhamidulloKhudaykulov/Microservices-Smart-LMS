using HomeworkModule.Infrastructure.EfConfigurations;
using HomeworkModule.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HomeworkModule.Infrastructure.Persistence;

public class HomeworkDbContext : DbContext
{
    public HomeworkDbContext(DbContextOptions<HomeworkDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HomeworkConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
