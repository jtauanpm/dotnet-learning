using System.Data.Common;
using DevIO.EfCore.Dominando.MultiTenancy.Providers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevIO.EfCore.Dominando.MultiTenancy.EFCore.Interceptors;

public class StrategySchemaInterceptor : DbCommandInterceptor
{
    private readonly TenantDataProvider _tenantDataProvider;

    public StrategySchemaInterceptor(TenantDataProvider tenantDataProvider)
    {
        _tenantDataProvider = tenantDataProvider;
    }

    public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, 
        InterceptionResult<DbDataReader> result)
    {
        ReplaceSchema(command);
        return base.ReaderExecuting(command, eventData, result);
    }

    private void ReplaceSchema(DbCommand command)
    {
        // FROM PRODUCTS -> FROM [tenant-1].PRODUCTS 
        command.CommandText = command.CommandText
            .Replace("FROM ", $" FROM [{_tenantDataProvider.TenantId}].")
            .Replace("JOIN ", $" JOIN [{_tenantDataProvider.TenantId}].");
    }
}