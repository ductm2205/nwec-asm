namespace quizzapp.data.Infrastructure.Repository;

public interface IBaseRepository<T>
{
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    int Add(T entity);
    bool Update(T entity);
    bool Delete(Guid id);
    bool Delete(T entity);
}