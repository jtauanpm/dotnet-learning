using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace DevIO.EfCore.Dominando.Data.QuerySqlGenerator;

public class CustomSqlServerQueryGeneratorFactory : SqlServerQuerySqlGeneratorFactory
{
    private readonly QuerySqlGeneratorDependencies _dependencies;
    private readonly IRelationalTypeMappingSource _typeMappingSource;
    private readonly ISqlServerSingletonOptions _sqlServerSingletonOptions;
    
    public CustomSqlServerQueryGeneratorFactory(QuerySqlGeneratorDependencies dependencies, 
        IRelationalTypeMappingSource typeMappingSource, ISqlServerSingletonOptions sqlServerSingletonOptions) 
        : base(dependencies, typeMappingSource, sqlServerSingletonOptions)
    {
        _dependencies = dependencies;
        _typeMappingSource = typeMappingSource;
        _sqlServerSingletonOptions = sqlServerSingletonOptions;
    }

    public override Microsoft.EntityFrameworkCore.Query.QuerySqlGenerator Create()
    {
        return new CustomSqlServerQuerySqlGenerator(_dependencies, _typeMappingSource, _sqlServerSingletonOptions);
    }
}