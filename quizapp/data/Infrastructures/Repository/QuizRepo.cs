using data.Context;
using models.Common;

namespace data.Infrastructures.Repository;

public class QuizRepo(AppDbContext context) : IQuizRepo
{
    private readonly AppDbContext _context = context;

    public int Add(Quiz entity)
    {
        _context.Set<Quiz>().Add(entity);
        return _context.SaveChanges();
    }

    public bool Delete(Guid id)
    {
        var target = GetById(id);

        if (target == null)
        {
            return false;
        }

        _context.Set<Quiz>().Remove(target);

        return _context.SaveChanges() > 0;
    }

    public bool Delete(Quiz entity)
    {
        if (entity == null)
        {
            return false;
        }

        _context.Set<Quiz>().Remove(entity);

        return _context.SaveChanges() > 0;
    }

    public IEnumerable<Quiz> GetAll()
    {
        return [.. _context.Set<Quiz>()];
    }

    public Quiz? GetById(Guid Id)
    {
        return _context.Set<Quiz>().Find(Id);
    }

    public bool Update(Quiz entity)
    {

        if (entity == null)
        {
            return false;
        }

        _context.Set<Quiz>().Update(entity);

        return _context.SaveChanges() > 0;
    }
}
