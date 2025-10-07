using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Data;

public static class UserDefinedFunctions
{
    [DbFunction(name: "LEFT", IsBuiltIn = true)]
    public static string Left(string dados, int quantidade)
    {
        throw new NotImplementedException();
    }

    public static void RegisterFunctions(this ModelBuilder modelBuilder)
    {
        var functions = typeof(UserDefinedFunctions).GetMethods()
            .Where(m => Attribute.IsDefined(m, typeof(DbFunctionAttribute)));

        foreach (var function in functions)
        {
            modelBuilder.HasDbFunction(function);
        }
    }
}