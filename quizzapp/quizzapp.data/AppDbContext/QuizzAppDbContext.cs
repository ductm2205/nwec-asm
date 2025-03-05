using System;
using Microsoft.EntityFrameworkCore;
using quizzapp.model.Auth;
using quizzapp.model.Model;
using quizzapp.model.Relation;

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

        // User & Role
        modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });
        modelBuilder.Entity<UserRole>().HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(u => u.UserId);
        modelBuilder.Entity<UserRole>().HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(r => r.RoleId);

        // User & Quiz
        modelBuilder.Entity<UserQuiz>().HasKey(userQuiz => userQuiz.Id);

        modelBuilder.Entity<UserQuiz>()
        .HasOne(userQuiz => userQuiz.User)
        .WithMany(user => user.UserQuizzes)
        .HasForeignKey(userQuiz => userQuiz.UserId);

        modelBuilder.Entity<UserQuiz>()
        .HasOne(userQuiz => userQuiz.Quiz)
        .WithMany(quiz => quiz.UserQuizzes)
        .HasForeignKey(userQuiz => userQuiz.QuizId);

        // Quiz's Answers
        modelBuilder.Entity<UserAnswer>().HasKey(userAnswer => userAnswer.Id);

        modelBuilder.Entity<UserAnswer>()
        .HasOne(answer => answer.UserQuiz)
        .WithMany(quiz => quiz.UserAnswers)
        .HasForeignKey(answer => answer.UserQuizId);


        base.OnModelCreating(modelBuilder);
    }


    public DbSet<Quiz> Quizzes { get; set; }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

}
