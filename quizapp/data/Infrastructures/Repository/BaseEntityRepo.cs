using data.Context;
using Microsoft.EntityFrameworkCore;
using models.Base;

namespace data.Infrastructures.Repository;

public class BaseEntityRepo<T>(AppDbContext context) : BaseItemRepo<T>(context), IBaseEntityRepo<T> where T : BaseEntity
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<T> _set = context.Set<T>();

    public bool Delete(Guid Id)
    {
        var target = _set.Find(Id);

        if (target == null)
        {
            return false;
        }

        _set.Remove(target);

        return _context.SaveChanges() > 0;
    }

    public T? GetById(Guid Id)
    {
        return _set.Find(Id);
    }

    public async Task<T?> GetByIdAsync(Guid Id)
    {
        return await _set.FindAsync(Id);
    }
}
