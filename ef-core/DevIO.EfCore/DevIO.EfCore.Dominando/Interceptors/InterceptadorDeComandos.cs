using System.Data.Common;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevIO.EfCore.Dominando.Interceptors;

public class InterceptadorDeComandos : DbCommandInterceptor
{
    // Regex para encontrar alias de tabela após o FROM
    private static readonly Regex _tableRegex =
        new Regex(
            @"(?<tableAlias>FROM\s+\[.*?\]\.?\[.*?\]\sAS\s\[.*?\])(?!\sWITH\s\(NOLOCK\))",
            RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled
        );

    public override InterceptionResult<DbDataReader> ReaderExecuting(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result)
    {
        UsarNoLock(command);
        return result;
    }

    public override ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(
        DbCommand command,
        CommandEventData eventData,
        InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = default)
    {
        UsarNoLock(command);
        return new ValueTask<InterceptionResult<DbDataReader>>(result);
    }

    private static void UsarNoLock(DbCommand command)
    {
        if (!command.CommandText.Contains("WITH (NOLOCK)"))
        {
            command.CommandText = _tableRegex.Replace(
                command.CommandText,
                "${tableAlias} WITH (NOLOCK)"
            );
        }
    }
}