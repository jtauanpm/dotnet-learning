namespace DevIO.EfCore.Dominando.MultiTenancy.Extensions;

public static class HttpContextExtensions
{
    public static string ExtractTenantIdFromPath(this HttpContext context)
    {
        var tenant = context.Request.Path.Value.Split("/", StringSplitOptions.RemoveEmptyEntries)[0];
        return tenant;
    }
}