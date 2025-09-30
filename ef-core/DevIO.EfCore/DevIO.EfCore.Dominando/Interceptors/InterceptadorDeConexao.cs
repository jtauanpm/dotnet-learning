using System.Data.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevIO.EfCore.Dominando.Interceptors;

public class InterceptadorDeConexao : DbConnectionInterceptor
{
    public override InterceptionResult ConnectionOpening(
        DbConnection connection,
        ConnectionEventData eventData,
        InterceptionResult result)
    {
        Console.WriteLine("Entrei no método ConnectionOpening");
        
        // var connectionString = ((SqlConnection)connection).ConnectionString;
        // Console.WriteLine("ConnectionString original: " + connectionString);
        //
        // var connectionStringBuilder = new SqlConnectionStringBuilder(connectionString)
        // {
        //     ApplicationName = "CursoEFCore"
        //     // DataSource = "IP Segundo Servidor" // se quiser mudar o servidor
        // };
        //
        // connection.ConnectionString = connectionStringBuilder.ToString();
        // Console.WriteLine("ConnectionString modificada: " + connection.ConnectionString);

        return result;
    }
}