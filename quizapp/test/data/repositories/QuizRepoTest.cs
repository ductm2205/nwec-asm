using data.Context;
using data.Infrastructures.Repository;
using Microsoft.EntityFrameworkCore;
using models.Common;

namespace test.data.repositories;

[TestFixture]
public class QuizRepoTest
{
    public required AppDbContext _context;
    public required QuizRepo _quizRepo;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;

        _context = new AppDbContext(options);

        _quizRepo = new QuizRepo(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void Add_Should_Add_Quiz_And_Return_SaveChanges_Result()
    {
        var quiz = new Quiz { Id = Guid.NewGuid(), Title = "Sample Quiz", Duration = 60 };


        var res = _quizRepo.Add(quiz);

        Assert.Multiple(() =>
        {
            Assert.That(res, Is.EqualTo(1));
            Assert.That(_context.Quizzes.Count(), Is.EqualTo(1));
        });

        // remove from the initial quizzes set
        _quizRepo.Delete(quiz);
        _context.SaveChanges();
    }

    [Test]
    public void DeleteById_Should_Remove_Quiz_If_Exists()
    {
        var quiz = new Quiz { Id = Guid.NewGuid(), Title = "Sample Quiz", Duration = 60 };
        _context.Quizzes.Add(quiz);
        _context.SaveChanges();

        var res = _quizRepo.Delete(quiz);

        Assert.That(res, Is.EqualTo(true));
    }

    [Test]
    public void GetAll_Should_Return_All_Quizzes()
    {
        _context.Quizzes.Add(new Quiz { Id = Guid.NewGuid(), Title = "Quiz 1", Duration = 60 });
        _context.Quizzes.Add(new Quiz { Id = Guid.NewGuid(), Title = "Quiz 2", Duration = 120 });
        _context.SaveChanges();

        var result = _quizRepo.GetAll();

        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public void GetById_Should_Return_Quiz_If_Exists()
    {
        var quiz = new Quiz { Id = Guid.NewGuid(), Title = "Sample Quiz", Duration = 60 };
        _context.Quizzes.Add(quiz);
        _context.SaveChanges();

        var result = _quizRepo.GetById(quiz.Id);

        Assert.Multiple(() =>
        {
            Assert.That(quiz.Id, Is.EqualTo(result?.Id));
            Assert.That(result, Is.Not.Null);
        });
    }
}
