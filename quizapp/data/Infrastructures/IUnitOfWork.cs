using data.Context;
using data.Infrastructures.Repository;
using models.Auth;
using models.Base;
using models.Common;
using models.Relationship;

namespace data.Infrastructures;

public interface IUnitOfWork : IDisposable
{
    AppDbContext Context { get; }

    IBaseItemRepo<T> BaseItemRepo<T>() where T : class, IBaseItem;
    IBaseEntityRepo<T> BaseEntityRepo<T>() where T : BaseEntity;

    IBaseEntityRepo<Quiz> QuizRepo { get; }
    IBaseEntityRepo<Question> QuestionRepo { get; }
    IBaseEntityRepo<Answer> AnswerRepo { get; }

    IBaseItemRepo<UserQuiz> UserQuizRepo { get; }
    IBaseItemRepo<UserAnswer> UserAnswerRepo { get; }

    IBaseItemRepo<User> UserRepo { get; }
    IBaseItemRepo<Role> RoleRepo { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    int SaveChanges();
}