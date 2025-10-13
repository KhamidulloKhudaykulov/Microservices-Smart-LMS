using Microsoft.EntityFrameworkCore;
using TeacherService.Persistence.EfConfigurations;
using TeacherService.Persistence.Extensions;

namespace TeacherService.Persistence;

public class TeacherServiceDbContext
    : DbContext
{
    public TeacherServiceDbContext(DbContextOptions<TeacherServiceDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TeacherConfiguration());

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
