using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using quizzapp.data.AppDbContext;
using quizzapp.data.Infrastructure.Repository;
using quizzapp.model.Base;
using quizzapp.model.Model;

namespace quizzapp.data.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly QuizzAppDbContext _context;
    private bool _disposed = false;

    public UnitOfWork(QuizzAppDbContext context)
    {
        _context = context;
    }
    public QuizzAppDbContext Context => _context;

    private IBaseRepository<Quiz>? _quizRepo;
    public IBaseRepository<Quiz> QuizRepository => _quizRepo ??= new BaseRepository<Quiz>(_context);
    private IBaseRepository<Question>? _questionRepo;
    public IBaseRepository<Question> QuestionRepository => _questionRepo ??= new BaseRepository<Question>(_context);
    private IBaseRepository<Answer>? _answerRepo;
    public IBaseRepository<Answer> AnswerRepository => _answerRepo ??= new BaseRepository<Answer>(_context);

    public void Dispose(bool isDisposing)
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


    public async Task<int> SaveChangesAsync()
    {
        BeforeSaveChange();
        return await _context.SaveChangesAsync();
    }

    private void BeforeSaveChange()
    {
        var entities = _context.ChangeTracker.Entries().Where(x => x.Entity is IEntity && x.State == Microsoft.EntityFrameworkCore.EntityState.Added || x.State == Microsoft.EntityFrameworkCore.EntityState.Modified);

        foreach (var entity in entities)
        {
            var baseEntity = (BaseEntity)entity.Entity;

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

    IBaseRepository<T> IUnitOfWork.BaseRepository<T>()
    {
        throw new NotImplementedException();
    }
}

public interface IUnitOfWork : IDisposable
{
    QuizzAppDbContext Context { get; }

    IBaseRepository<Quiz> QuizRepository { get; }
    IBaseRepository<Question> QuestionRepository { get; }
    IBaseRepository<Answer> AnswerRepository { get; }

    IBaseRepository<T> BaseRepository<T>() where T : class, IEntity;

    Task<int> SaveChangesAsync();

    int SaveChanges();
}