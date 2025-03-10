using System;
using Microsoft.EntityFrameworkCore;
using models.Common;

namespace data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
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
    }

    public DbSet<Quiz> Quizzes { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
}
