using LessonModule.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LessonModule.Infrastructure.Persistence;

public class LessonDbContext : DbContext
{
    public LessonDbContext(DbContextOptions<LessonDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
