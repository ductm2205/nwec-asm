using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using models.Auth;
using models.Common;
using models.Relationship;

namespace data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Quiz>(
            quiz => quiz.HasMany(quiz => quiz.Questions)
            .WithOne(question => question.Quiz)
            .HasForeignKey(question => question.QuizId)
        );

        builder.Entity<Question>(
            question => question.HasMany(q => q.Answers)
            .WithOne(ans => ans.Question)
            .HasForeignKey(ans => ans.QuestionId)
        );

        // users & roles
        builder.Entity<User>().ToTable("Users", "auth");
        builder.Entity<Role>().ToTable("Roles", "auth");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "auth");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "auth");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "auth");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "auth");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "auth");

        // user quiz
        builder.Entity<UserQuiz>().HasKey(uq => uq.QuizCode);

        // user quiz -> user
        builder.Entity<UserQuiz>()
        .HasOne(uq => uq.User)
        .WithMany(u => u.UserQuizzes)
        .HasForeignKey(uq => uq.UserId);

        // user quiz -> quiz
        builder.Entity<UserQuiz>()
        .HasOne(uq => uq.Quiz)
        .WithMany(q => q.UserQuizzes)
        .HasForeignKey(uq => uq.QuizId);
    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
}
