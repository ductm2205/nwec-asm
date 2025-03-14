using data.Context;
using data.Infrastructures.Repository;
using Microsoft.EntityFrameworkCore.Storage;
using models.Auth;
using models.Base;
using models.Common;
using models.Relationship;

namespace data.Infrastructures;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private bool _disposed = false;
    private IDbContextTransaction? _transaction;

    public AppDbContext Context => _context;

    private readonly IBaseEntityRepo<Quiz>? _quizRepo;
    public IBaseEntityRepo<Quiz> QuizRepo => _quizRepo ?? new BaseEntityRepo<Quiz>(_context);

    private readonly IBaseEntityRepo<Question>? _questionRepo;
    public IBaseEntityRepo<Question> QuestionRepo => _questionRepo ?? new BaseEntityRepo<Question>(_context);

    private readonly IBaseEntityRepo<Answer>? _answerRepo;
    public IBaseEntityRepo<Answer> AnswerRepo => _answerRepo ?? new BaseEntityRepo<Answer>(_context);

    private readonly IBaseItemRepo<UserQuiz>? _uqRepo;
    public IBaseItemRepo<UserQuiz> UserQuizRepo => _uqRepo ?? new BaseItemRepo<UserQuiz>(_context);

    private readonly IBaseItemRepo<UserAnswer>? _uaRepo;
    public IBaseItemRepo<UserAnswer> UserAnswerRepo => _uaRepo ?? new BaseItemRepo<UserAnswer>(_context);

    private readonly IBaseItemRepo<User>? _userRepo;
    public IBaseItemRepo<User> UserRepo => _userRepo ?? new BaseItemRepo<User>(_context);

    private readonly IBaseItemRepo<Role>? _roleRepo;
    public IBaseItemRepo<Role> RoleRepo => _roleRepo ?? new BaseItemRepo<Role>(_context);

    public IBaseEntityRepo<T> BaseEntityRepo<T>() where T : BaseEntity
    {
        return new BaseEntityRepo<T>(_context);
    }

    public IBaseItemRepo<T> BaseItemRepo<T>() where T : class, IBaseItem
    {
        return new BaseItemRepo<T>(_context);
    }
    public async Task BeginTransactionAsync()
    {
        _transaction ??= await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch (System.Exception)
            {
                await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }


    protected virtual void Dispose(bool isDisposing)
    {
        if (!_disposed)
        {
            if (isDisposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }



    public int SaveChanges()
    {
        BeforeSaveChange();
        return _context.SaveChanges();
    }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        BeforeSaveChange();
        return await _context.SaveChangesAsync(cancellationToken);
    }

    private void BeforeSaveChange()
    {
        var entities = _context.ChangeTracker.Entries().Where(x => x.Entity is BaseItem && x.State == Microsoft.EntityFrameworkCore.EntityState.Added || x.State == Microsoft.EntityFrameworkCore.EntityState.Modified);

        foreach (var entity in entities)
        {
            var baseEntity = (BaseItem)entity.Entity;

            switch (entity.State)
            {
                case Microsoft.EntityFrameworkCore.EntityState.Added:
                    baseEntity.CreatedAt = DateTime.Now;
                    break;
                case Microsoft.EntityFrameworkCore.EntityState.Modified:
                    baseEntity.UpdatedAt = DateTime.Now;
                    break;
            }
        }
    }
}
