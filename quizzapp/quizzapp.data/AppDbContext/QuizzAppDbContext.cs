using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using quizzapp.model.Auth;
using quizzapp.model.Model;
using quizzapp.model.Relation;

namespace quizzapp.data.AppDbContext;

public class QuizzAppDbContext(DbContextOptions<QuizzAppDbContext> options) : IdentityDbContext<User, Role, Guid>(options)
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Quiz>(quizz =>
        {
            quizz
            .HasMany(quizz => quizz.Questions)
            .WithOne(question => question.Quiz)
            .HasForeignKey(question => question.QuizId)
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

}
