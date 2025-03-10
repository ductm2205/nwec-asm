using System.Linq.Expressions;
using models.Base;

namespace data.Infrastructures.Repository;

public interface IBaseItemRepo<T> where T : class, IBaseItem
{
    IEnumerable<T> GetAll();
    Task<IEnumerable<T>> GetAllAsync();
    void Add(T entity);
    bool Update(T entity);
    bool Delete(T entity);
    IQueryable<T> GetQuery();
    IQueryable<T> GetQuery(Expression<Func<T, bool>> predicate);
    IQueryable<T> Get(
    Expression<Func<T, bool>>? filter = null,
    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
    string includeProperties = ""
    );
}