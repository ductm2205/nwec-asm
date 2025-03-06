using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using quizzapp.data.AppDbContext;
using quizzapp.model.Base;

namespace quizzapp.data.Infrastructure.Repository;

public class BaseRepository<T>(QuizzAppDbContext context) : IBaseRepository<T> where T : class, IEntity
{
    private readonly QuizzAppDbContext _context = context;

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        return entity;
    }

    public void Delete(Guid id)
    {
        var target = GetById(id);
        Delete(target);
    }

    public void Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public IQueryable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(
            [','], StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return orderBy != null ? orderBy(query) : query;
    }

    public IEnumerable<T> GetAll()
    {
        return [.. _context.Set<T>()];
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public T? GetById(Guid id)
    {
        return _context.Set<T>().Find(id);
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public IQueryable<T> GetQuery()
    {
        return _context.Set<T>().AsQueryable<T>();
    }

    public IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
    {
        return GetQuery().Where(where);
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

}
