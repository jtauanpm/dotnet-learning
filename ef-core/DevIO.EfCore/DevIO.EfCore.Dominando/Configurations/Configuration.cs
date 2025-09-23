using Microsoft.Extensions.Configuration;

namespace DevIO.EfCore.Dominando.Configurations;

public static class Configuration
{
    public static IConfiguration GetConfiguration()
    {
        return new ConfigurationBuilder()
            .AddUserSecrets<Program>(true)
            .Build();
    }
}