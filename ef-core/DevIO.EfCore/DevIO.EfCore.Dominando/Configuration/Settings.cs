using Microsoft.Extensions.Configuration;

namespace DevIO.EfCore.Dominando.Configuration;

public static class Settings
{
    public static IConfiguration Configuration { get; set; }
}