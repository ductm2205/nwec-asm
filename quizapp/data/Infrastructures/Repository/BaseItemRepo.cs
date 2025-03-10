using System;
using models.Base;

namespace data.Infrastructures.Repository;

public class BaseItemRepo<T> : IBaseItemRepo<T> where T : class, IBaseItem
{
    public int Add(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<int> AddAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public bool Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public bool Update(T entity)
    {
        throw new NotImplementedException();
    }
}

public interface IBaseItemRepo<T> where T : class, IBaseItem
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    int Add(T entity);
    Task<int> AddAsync(T entity);
    bool Update(T entity);
    bool Delete(T entity);
}