using models.Base;

namespace data.Infrastructures.Repository;

public interface IBaseEntityRepo<T> : IBaseItemRepo<T> where T : BaseEntity
{

}