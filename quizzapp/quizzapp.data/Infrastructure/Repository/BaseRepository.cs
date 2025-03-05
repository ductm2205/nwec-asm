using System;
using quizzapp.data.AppDbContext;
using quizzapp.model.Base;

namespace quizzapp.data.Infrastructure.Repository;

public class BaseRepository<T>(QuizzAppDbContext context) : IBaseRepository<T> where T : class, IEntity
{
    private readonly QuizzAppDbContext _context = context;

    public int Add(T entity)
    {
        _context.Set<T>().Add(entity);
        return 1;
    }

    public bool Delete(Guid id)
    {
        var target = GetById(id);

        if (target == null)
        {
            return false;
        }

        return Delete(target);
    }

    public bool Delete(T entity)
    {
        if (entity == null)
        {
            return false;
        }

        _context.Set<T>().Remove(entity);
        return true;
    }

    public IEnumerable<T> GetAll()
    {
        return [.. _context.Set<T>()];
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>().Find(id);
    }

    public bool Update(T entity)
    {
        throw new NotImplementedException();
    }
}
