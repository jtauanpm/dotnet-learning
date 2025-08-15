using DevIO.PrimeiraApi.App.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddApiConfig()
    .AddCorsConfig()
    .AddSwaggerConfig()
    .AddEfCoreConfig()
    .AddIdentityConfig();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors("Development");
}
else
{
    app.UseCors("Production");
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World! \nAcesse /swagger para mais informações");

app.Run();