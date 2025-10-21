using DevIO.EfCore.Dominando.MultiTenancy.Extensions;
using DevIO.EfCore.Dominando.MultiTenancy.Providers;

namespace DevIO.EfCore.Dominando.MultiTenancy.Middlewares;

public class TenantResolverMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var tenant = context.RequestServices.GetRequiredService<TenantDataProvider>();
        tenant.TenantId = context.ExtractTenantIdFromPath();
        await next(context);
    }
}