using System.Linq.Expressions;
using models.Base;

namespace data.Infrastructures.Repository;

public interface IBaseItemRepo<T> where T : class, IBaseItem
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    T? GetById(Guid Id);
    Task<T?> GetByIdAsync(Guid Id);
    bool Add(T entity);
    bool Update(T entity);
    bool Delete(Guid Id);
    bool Delete(T entity);
    IQueryable<T> GetQuery();
    IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get(
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    string includeProperties = ""
    );
}