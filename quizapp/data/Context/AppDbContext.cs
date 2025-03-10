using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using models.Auth;
using models.Common;
using models.Relationship;

namespace data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Quiz>(
            quiz => quiz.HasMany(quiz => quiz.Questions)
            .WithOne(question => question.Quiz)
            .HasForeignKey(question => question.QuizId)
        );

        modelBuilder.Entity<Question>(
            question => question.HasMany(q => q.Answers)
            .WithOne(ans => ans.Question)
            .HasForeignKey(ans => ans.QuestionId)
        );

        // users & roles
        modelBuilder.Entity<User>().ToTable("Users", "auth");
        modelBuilder.Entity<Role>().ToTable("Roles", "auth");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "auth");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "auth");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "auth");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "auth");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "auth");

        // user quiz
        modelBuilder.Entity<UserQuiz>().HasKey(uq => uq.QuizCode);

        // user quiz -> user
        modelBuilder.Entity<UserQuiz>()
        .HasOne(uq => uq.User)
        .WithMany(u => u.UserQuizzes)
        .HasForeignKey(uq => uq.UserId);

        // user quiz -> quiz
        modelBuilder.Entity<UserQuiz>()
        .HasOne(uq => uq.Quiz)
        .WithMany(q => q.UserQuizzes)
        .HasForeignKey(uq => uq.QuizId);
    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
}
