using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevIO.PrimeiraApi.App.Entities;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Produto> Produtos { get; set; }
}