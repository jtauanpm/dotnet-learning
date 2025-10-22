using DevIO.EfCore.Dominando.MultiTenancy.Entities;
using DevIO.EfCore.Dominando.MultiTenancy.Providers;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.MultiTenancy.EFCore;

public class MultiTenancyDbContext : DbContext
{
    public DbSet<Person> People { get; set; }
    public DbSet<Product> Products { get; set; }
    
    private readonly TenantDataProvider _tenantDataProvider;

    public MultiTenancyDbContext(DbContextOptions<MultiTenancyDbContext> options, TenantDataProvider tenantDataProvider) : base(options)
    {
        _tenantDataProvider = tenantDataProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, Name = "Person 1", TenantId = "tenant-1" },
            new Person { Id = 2, Name = "Person 2", TenantId = "tenant-2" },
            new Person { Id = 3, Name = "Person 3", TenantId = "tenant-2" });
        
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Description = "Description 1", TenantId = "tenant-1" },
            new Product { Id = 2, Description = "Description 2", TenantId = "tenant-2" },
            new Product { Id = 3, Description = "Description 3", TenantId = "tenant-2" });
        
        //TODO: do this dynamically for all entities that inherit from BaseEntity
        modelBuilder.Entity<Person>().HasQueryFilter(e => e.TenantId == _tenantDataProvider.TenantId);
        modelBuilder.Entity<Product>().HasQueryFilter(e => e.TenantId == _tenantDataProvider.TenantId);
    }
}