using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevIO.EfCore.Dominando.Data.Diagnostics;

public class CustomDiagnosticInterceptor : IObserver<KeyValuePair<string, object>>
{
    private static readonly Regex _tableAliasRegex = new Regex(
        @"(?<tableAlias>FROM +(\[.*\]\.)?(\[.*\]) AS (\[.*\]))(?! WITH \(NOLOCK\))",
        RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(KeyValuePair<string, object> value)
    {
        if (value.Key == RelationalEventId.CommandExecuting.Name)
        {
            var command = ((CommandEventData)value.Value).Command;

            if (!command.CommandText.Contains("WITH (NOLOCK)", StringComparison.OrdinalIgnoreCase))
            {
                command.CommandText = _tableAliasRegex.Replace(
                    command.CommandText,
                    "${tableAlias} WITH (NOLOCK)");
                
                Console.WriteLine(command.CommandText);
            }
        }
    }
}

public class CustomInterceptorListener : IObserver<System.Diagnostics.DiagnosticListener>
{
    private readonly CustomDiagnosticInterceptor _interceptor = new CustomDiagnosticInterceptor();

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }

    public void OnNext(System.Diagnostics.DiagnosticListener listener)
    {
        if (listener.Name == DbLoggerCategory.Name)
        {
            listener.Subscribe(_interceptor!);
        }
    }
}