using DevIO.EfCore.Dominando.MultiTenancy.EFCore;
using DevIO.EfCore.Dominando.MultiTenancy.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("Dev_IO_EfCore_Dominando_MultiTenancy");
builder.Services.AddDbContext<MultiTenancyDbContext>(o => 
    o.UseSqlServer(connectionString)
    .EnableSensitiveDataLogging()
    .LogTo(Console.WriteLine));

var app = builder.Build();

DatabaseInitialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void DatabaseInitialize(IApplicationBuilder appBuilder)
{
    using var db = appBuilder
        .ApplicationServices
        .CreateScope()
        .ServiceProvider
        .GetRequiredService<MultiTenancyDbContext>();
    
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();

    for (var i = 0; i < 5; i++)
    {
        db.People.Add(new Person { Name = $"Person {i}" });
        db.Products.Add(new Product { Description = $"Product {i}" });
    }

    db.SaveChanges();
}