using data.Context;
using Microsoft.EntityFrameworkCore;
using models.Base;

namespace data.Infrastructures.Repository;

public class BaseEntityRepo<T>(AppDbContext context) : BaseItemRepo<T>(context), IBaseEntityRepo<T> where T : BaseEntity
{
    private readonly AppDbContext _context = context;
    private readonly DbSet<T> _set = context.Set<T>();
}
