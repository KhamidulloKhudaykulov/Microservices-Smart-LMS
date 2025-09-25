using Microsoft.EntityFrameworkCore;

namespace PaymentService.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options) { }
}
