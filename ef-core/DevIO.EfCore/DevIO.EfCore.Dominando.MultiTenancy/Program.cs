using DevIO.EfCore.Dominando.MultiTenancy.EFCore;
using DevIO.EfCore.Dominando.MultiTenancy.EFCore.Interceptors;
using DevIO.EfCore.Dominando.MultiTenancy.Entities;
using DevIO.EfCore.Dominando.MultiTenancy.Middlewares;
using DevIO.EfCore.Dominando.MultiTenancy.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.AddScoped<StrategySchemaInterceptor>();

var connectionString = builder.Configuration.GetConnectionString("Dev_IO_EfCore_Dominando_MultiTenancy");
// builder.Services.AddDbContext<MultiTenancyDbContext>(o => 
//     o.UseSqlServer(connectionString)
//     .EnableSensitiveDataLogging()
//     .LogTo(Console.WriteLine));

builder.Services.AddDbContext<MultiTenancyDbContext>((provider, options) =>
{
    options.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging()
        // For using custom cache model, caching by TenantData for reusing
        .ReplaceService<IModelCacheKeyFactory, ModelCacheKeyFactory>()
        .LogTo(Console.WriteLine);
    
    // We retrieve instance at runtime so that TenantDataProvider is resolved
    // var interceptor = provider.GetRequiredService<StrategySchemaInterceptor>();
    // options.AddInterceptors(interceptor);
});

builder.Services.AddScoped<TenantDataProvider>();
builder.Services.AddTransient<TenantResolverMiddleware>();

var app = builder.Build();

// DatabaseInitialize(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<TenantResolverMiddleware>();

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