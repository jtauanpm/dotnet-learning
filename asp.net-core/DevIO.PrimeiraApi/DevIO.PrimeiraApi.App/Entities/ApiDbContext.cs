using Microsoft.EntityFrameworkCore;

namespace DevIO.PrimeiraApi.App.Entities;

public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options);