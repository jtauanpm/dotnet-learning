using DevIO.EfCore.Dominando.MultiTenancy.EFCore;
using DevIO.EfCore.Dominando.MultiTenancy.EFCore.Interceptors;
using DevIO.EfCore.Dominando.MultiTenancy.Entities;
using DevIO.EfCore.Dominando.MultiTenancy.Extensions;
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

// Strategy 1 - TenantId column identifier for each table
// builder.Services.AddDbContext<MultiTenancyDbContext>(o => 
//     o.UseSqlServer(connectionString)
//     .EnableSensitiveDataLogging()
//     .LogTo(Console.WriteLine));

// Strategy 2 - spliting by Schema - TenantId (using ModelCache by Schema or using command interceptor to add schema in queries)
// builder.Services.AddDbContext<MultiTenancyDbContext>((provider, options) =>
// {
//     options.UseSqlServer(connectionString)
//         .EnableSensitiveDataLogging()
//         // For using custom cache model, caching by TenantData for reusing
//         .ReplaceService<IModelCacheKeyFactory, ModelCacheKeyFactory>()
//         .LogTo(Console.WriteLine);
//     
//     // We retrieve instance at runtime so that TenantDataProvider is resolved
//     // var interceptor = provider.GetRequiredService<StrategySchemaInterceptor>();
//     // options.AddInterceptors(interceptor);
// });
// builder.Services.AddTransient<TenantResolverMiddleware>();
// builder.Services.AddScoped<TenantDataProvider>();

// Strategy 3 - A DB for each Tenant
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<MultiTenancyDbContext>(provider =>
{
    var optionsBuilder = new DbContextOptionsBuilder<MultiTenancyDbContext>();
    
    var httpContext = provider.GetService<IHttpContextAccessor>()?.HttpContext;
    var tenantId = httpContext?.ExtractTenantIdFromPath();
    
    var connectionString = builder.Configuration.GetConnectionString(tenantId);
    
    optionsBuilder
        .UseSqlServer(connectionString)
        .LogTo(Console.WriteLine)
        .EnableSensitiveDataLogging();
    
    return new MultiTenancyDbContext(optionsBuilder.Options);
});


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