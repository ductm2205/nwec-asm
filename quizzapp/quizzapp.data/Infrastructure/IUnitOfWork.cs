using quizzapp.data.AppDbContext;
using quizzapp.data.Infrastructure.Repository;
using quizzapp.model.Auth;
using quizzapp.model.Base;
using quizzapp.model.Model;

namespace quizzapp.data.Infrastructure;

public interface IUnitOfWork : IDisposable
{
    QuizzAppDbContext Context { get; }

    IBaseRepository<Quiz> QuizRepository { get; }
    IBaseRepository<Question> QuestionRepository { get; }
    IBaseRepository<Answer> AnswerRepository { get; }

    IBaseRepository<Role> RoleRepository { get; }
    IBaseRepository<User> UserRepository { get; }

    IBaseRepository<T> BaseRepository<T>() where T : class, IEntity;

    Task<int> SaveChangesAsync();

    int SaveChanges();
}