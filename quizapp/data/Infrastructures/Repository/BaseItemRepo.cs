using System.Linq.Expressions;
using data.Context;
using Microsoft.EntityFrameworkCore;
using models.Base;

namespace data.Infrastructures.Repository;

public class BaseItemRepo<T>(AppDbContext context) : IBaseItemRepo<T> where T : class, IBaseItem
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<T> _set = context.Set<T>();

    public void Add(T entity)
    {
        _set.Add(entity);
    }

    public bool Delete(T entity)
    {
        if (entity == null)
        {
            return false;
        }

        _set.Remove(entity);

        return _context.SaveChanges() > 0;
    }

    public IQueryable<T> Get(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = ""
    )
    {
        var query = _set.AsQueryable<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (!string.IsNullOrEmpty(includeProperties))
        {
            query = query.Include(includeProperties);
        }

        return query;
    }

    public IEnumerable<T> GetAll()
    {
        return [.. _set];
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _set.AsQueryable().ToListAsync();
    }

    public IQueryable<T> GetQuery()
    {
        return _set.AsQueryable<T>();
    }

    public IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate)
    {
        return _set.AsQueryable<T>().Where(predicate);
    }

    public bool Update(T entity)
    {
        if (entity == null)
        {
            return false;
        }

        _set.Update(entity);
        return _context.SaveChanges() > 0;
    }
}
