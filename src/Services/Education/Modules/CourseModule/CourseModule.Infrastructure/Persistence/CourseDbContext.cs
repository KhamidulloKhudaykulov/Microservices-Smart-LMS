using Microsoft.EntityFrameworkCore;

namespace CourseModule.Infrastructure.Persistence;

public class CourseDbContext : DbContext
{
    public CourseDbContext(DbContextOptions<CourseDbContext> options)
        : base(options) { }
}
