using System;
using Microsoft.EntityFrameworkCore;
using quizzapp.model.Model;

namespace quizzapp.data.AppDbContext;

public class QuizzAppDbContext(DbContextOptions<QuizzAppDbContext> options) : DbContext(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quiz>(quizz =>
        {
            quizz
            .HasMany(quizz => quizz.Questions)
            .WithOne(question => question.Quiz)
            .HasForeignKey(question => question.QuizzId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Question>(question =>
        {
            question
            .HasMany(question => question.Answers)
            .WithOne(answer => answer.Question)
            .HasForeignKey(answer => answer.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Quiz> Quizes { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }
}
