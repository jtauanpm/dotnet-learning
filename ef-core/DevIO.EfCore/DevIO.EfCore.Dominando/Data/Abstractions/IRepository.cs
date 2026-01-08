using System.Collections;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Data.Abstractions
{
    public interface IRepository<T> : IQueryable<T>, IRepositoryBase<T>
    {
    
    }

    public interface IRepositoryBase<T> : IQueryable<T>, IAsyncEnumerable<T>
    {
    
    }

    public class EfRepository<T> : IRepository<T>
        where T : class
    {
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType { get; }
        public Expression Expression { get; }
        public IQueryProvider Provider { get; }
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }
    }
}
