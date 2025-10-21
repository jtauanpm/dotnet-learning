using DevIO.EfCore.Dominando.MultiTenancy.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.MultiTenancy.EFCore;

public class MultiTenancyDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Product> Products { get; set; }

    public MultiTenancyDbContext(DbContextOptions<MultiTenancyDbContext> options) : base(options)
    {
    }
}