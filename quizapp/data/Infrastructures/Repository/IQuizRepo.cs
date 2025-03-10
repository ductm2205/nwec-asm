using models.Common;

namespace data.Infrastructures.Repository;

public interface IQuizRepo
{
    IEnumerable<Quiz> GetAll();
    Quiz? GetById(Guid Id);
    int Add(Quiz entity);
    bool Update(Quiz entity);
    bool Delete(Guid id);
    bool Delete(Quiz entity);
}