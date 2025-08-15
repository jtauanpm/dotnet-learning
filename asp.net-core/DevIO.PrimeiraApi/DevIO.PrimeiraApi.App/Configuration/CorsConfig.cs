namespace DevIO.PrimeiraApi.App.Configuration;

public static class CorsConfig
{
    public static WebApplicationBuilder AddCorsConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("Development", devBuilder =>
                devBuilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            options.AddPolicy("Production", prodBuilder =>
                prodBuilder.WithOrigins("https://localhost:9000")
                    .WithMethods("POST")
                    .AllowAnyHeader());
        });
        
        return builder;
    }
}