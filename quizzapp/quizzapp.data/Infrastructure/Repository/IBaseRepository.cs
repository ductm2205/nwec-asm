using System.Linq.Expressions;

namespace quizzapp.data.Infrastructure.Repository;

public interface IBaseRepository<T>
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    T? GetById(Guid id);
    Task<T?> GetByIdAsync(Guid id);
    void Add(T entity);
    Task<T> AddAsync(T entity);
    void Update(T entity);
    void Delete(Guid id);
    void Delete(T entity);
    IQueryable<T> GetQuery();
    IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = ""
    );
}