using data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using models.Common;

namespace data.Seeder;

public class DatabaseSeeder
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        using var context = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()
        );

        if (context.Quizzes.Any() || context.Questions.Any() || context.Answers.Any())
        {
            return;
        }

        var quizzes = QuizSeeder(context);
        QuestionSeeder(context, quizzes);

    }

    private static List<Quiz> QuizSeeder(AppDbContext context)

    {
        var quizzes = new[]
        {
                new Quiz
                {
                    Title = "General Knowledge Quiz",
                    Description = "Test your knowledge on various topics.",
                    Duration = 900, // 15 minutes
                    ThumbnailUrl = "https://example.com/thumbnail1.jpg",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = false,
                },
                new Quiz
                {
                    Title = "Science & Technology Quiz",
                    Description = "A quiz about scientific facts and innovations.",
                    Duration = 1200, // 20 minutes
                    ThumbnailUrl = "https://example.com/thumbnail2.jpg",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = false
                },
                new Quiz
                {
                    Title = "Mathematics Quiz",
                    Description = "Challenge your math skills with these problems.",
                    Duration = 1800, // 30 minutes
                    ThumbnailUrl = "https://example.com/thumbnail3.jpg",
                    CreatedAt = DateTime.UtcNow,
                },
                new Quiz
                {
                    Title = "History Quiz",
                    Description = "A deep dive into historical events and figures.",
                    Duration = 1500, // 25 minutes
                    ThumbnailUrl = "https://example.com/thumbnail4.jpg",
                    CreatedAt = DateTime.UtcNow,
                },
                new Quiz
                {
                    Title = "Programming Quiz",
                    Description = "Test your coding knowledge in multiple languages.",
                    Duration = 2000, // 33 minutes
                    ThumbnailUrl = "https://example.com/thumbnail5.jpg",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = false
                }
        };
        context.Quizzes.AddRange(quizzes);
        context.SaveChanges();

        return [.. quizzes];
    }

    private static void QuestionSeeder(AppDbContext context, List<Quiz> quizzes)
    {
        var questions = new[]
        {
            // General Knowledge Quiz Question
            new Question
            {
                Content = "Which planet is known as the Red Planet?",
                QuestionType = QuestionType.SingleChoice,
                QuizId = quizzes[0].Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Answers =
                [
                    new Answer { Content = "Mars", IsCorrect = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "Venus", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "Jupiter", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                ]
            },

            // Science & Technology Quiz Question
            new Question
            {
                Content = "What is the chemical symbol for gold?",
                QuestionType = QuestionType.SingleChoice,
                QuizId = quizzes[1].Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Answers =
                [
                    new Answer { Content = "Au", IsCorrect = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "Ag", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "Fe", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                ]
            },

            // Mathematics Quiz Question
            new Question
            {
                Content = "What is the square root of 144?",
                QuestionType = QuestionType.SingleChoice,
                QuizId = quizzes[2].Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Answers =
                [
                    new Answer { Content = "12", IsCorrect = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "14", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "10", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                ]
            },

            // History Quiz Question
            new Question
            {
                Content = "In which year did World War II end?",
                QuestionType = QuestionType.SingleChoice,
                QuizId = quizzes[3].Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Answers =
                [
                    new Answer { Content = "1945", IsCorrect = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "1944", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "1946", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                ]
            },

            // Programming Quiz Question
            new Question
            {
                Content = "Which programming language is often used for web development?",
                QuestionType = QuestionType.SingleChoice,
                QuizId = quizzes[4].Id,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Answers =
                [
                    new Answer { Content = "JavaScript", IsCorrect = true, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "Assembly", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new Answer { Content = "COBOL", IsCorrect = false, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                ]
            }
        };

        context.Questions.AddRange(questions);
        context.SaveChanges();
    }

}
