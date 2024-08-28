using System.Linq.Expressions;
using Domain.Entities.Base;
using Domain.Interface.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infra.Data.Repository.Base;

public class BaseRepository<TContext> : IBaseRepository where TContext : DbContext
{
    private readonly TContext _context;

    public BaseRepository(TContext context)
    {
        _context = context;
    }

    public void Create<T>(T entity) where T : class
    {
        _context.Add(entity);
    }

    public T Query<T>(Expression<Func<T, bool>> filter, 
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null) where T : class
    {
        var query = _context.Set<T>()
                            .AsQueryable();
        
        if(includes != null)
            query = includes(query);
        
        return (T) query.Where(filter);
    }

    public void Update<T>(T entity) where T : class
    {
        _context.Update(entity);
    }

    public void Delete<T>(T entity) where T : class
    {
        _context.Remove(entity);
    }

    public bool SaveChanges(CancellationToken cancellationToken = default)
    {
        UpdateAuditDates();
        
        try
        {
            var result = _context.SaveChanges();
            return result >= 1;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    private void UpdateAuditDates()
    {
        var entityEntries = _context.ChangeTracker.Entries();

        foreach (var entry in entityEntries)
        {
            if (entry.State == EntityState.Added)
            {
                var entity = entry.Entity as BaseEntity;
                entity?.ChangeCreationDate(DateTimeOffset.UtcNow);
            }

            if (entry.State == EntityState.Modified)
            {
                var entity = entry.Entity as BaseEntity;
                entity?.ChangeUpdateDate(DateTimeOffset.UtcNow);
            }
        }
    }
}