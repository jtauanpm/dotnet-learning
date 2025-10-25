using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DevIO.EfCore.Dominando.MultiTenancy.EFCore.ModelFactory;

// public class StrategySchemaModelCacheKey : IModelCacheKeyFactory
// {
//     public object Create(DbContext context, bool designTime)
//     {
//         var model = new
//         {
//             Type = context.GetType(),
//             Schema = (context as MultiTenancyDbContext)?.TenantDataProvider.TenantId
//         };
//         
//         return model;
//     }
// }