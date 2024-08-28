using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Interface.Base;

public interface IBaseRepository
{
    public void Create<T>(T entity) where T : class;

    public T Query<T>(Expression<Func<T, bool>> filter, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null) where T : class;

    public void Update<T>(T entity) where T : class;
    public void Delete<T>(T entity) where T : class;
    public bool SaveChanges(CancellationToken cancellationToken = default);
}