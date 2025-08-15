using DevIO.PrimeiraApi.App.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevIO.PrimeiraApi.App.Configuration;

public static class EfCoreConfig
{
    public static WebApplicationBuilder AddEfCoreConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApiDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Dev_IO_PrimeiraAPI")));
        
        return builder;
    }
}