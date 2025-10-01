using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DevIO.EfCore.Dominando.Interceptors;

public class InterceptadorPersistencia : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        Console.WriteLine(eventData.Context.ChangeTracker.DebugView.LongView);
        
        return result;
    }
}