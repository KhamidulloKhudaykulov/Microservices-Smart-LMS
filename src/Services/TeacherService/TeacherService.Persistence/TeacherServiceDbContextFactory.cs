using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace TeacherService.Persistence;

public class TeacherServiceDbContextFactory : IDesignTimeDbContextFactory<TeacherServiceDbContext>
{
    public TeacherServiceDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultTeacherDbConnection");

        var optionsBuilder = new DbContextOptionsBuilder<TeacherServiceDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new TeacherServiceDbContext(optionsBuilder.Options);
    }
}