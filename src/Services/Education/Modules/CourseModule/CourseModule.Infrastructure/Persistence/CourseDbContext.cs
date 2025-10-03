using CourseModule.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CourseModule.Infrastructure.Persistence;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
