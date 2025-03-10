using models.Base;

namespace data.Infrastructures.Repository;

public interface IBaseEntityRepo<T> where T : BaseEntity
{
    T? GetById(Guid Id);
    Task<T?> GetByIdAsync(Guid Id);
    bool Delete(Guid Id);
}