using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevIO.EfCore.Dominando.Data.QuerySqlGenerator;

public class CustomSqlServerQuerySqlGenerator : SqlServerQuerySqlGenerator
{
    public CustomSqlServerQuerySqlGenerator(QuerySqlGeneratorDependencies dependencies,
        IRelationalTypeMappingSource typeMappingSource, ISqlServerSingletonOptions sqlServerSingletonOptions) : base(
        dependencies, typeMappingSource: typeMappingSource, sqlServerSingletonOptions: sqlServerSingletonOptions)
    {
    }

    protected override Expression VisitTable(TableExpression tableExpression)
    {
        var table = base.VisitTable(tableExpression);
        Sql.Append(" WITH (NOLOCK)");

        return table;
    }
}