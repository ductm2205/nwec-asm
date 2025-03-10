using System.Linq.Expressions;
using business.Commands.Users;
using business.Handlers.Users;
using data.Infrastructures;
using models.Auth;
using Moq;

namespace test.business.handlers.users;

[TestFixture]
public class CreateHandlerTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private CreateHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateHandler(_mockUnitOfWork.Object);
    }

    [Test]
    public async Task CreateHandler_Should_Create_New_UserAsync()
    {
        var command = new CreateCommand
        {
            UserName = "newuser",
            Email = "newuser@example.com",
            Password = "password",
            FirstName = "New",
            LastName = "User",
            DateOfBirth = DateTime.UtcNow.AddYears(-20)
        };

        // // assume the list is empty
        // _mockUnitOfWork.Setup(uow => uow.UserRepo.GetQuery(It.IsAny<Expression<Func<User, bool>>>()))
        // .Returns(new List<User>().AsQueryable());

        _mockUnitOfWork.Setup(uow => uow.UserRepo.Add(It.IsAny<User>())).Returns(true);
        _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task CreateHandler_Should_Return_False()
    {
        var command = new CreateCommand
        {
            UserName = "User 1",
            Email = "existinguser@example.com",
            Password = "password",
            FirstName = "New",
            LastName = "User",
            DateOfBirth = DateTime.UtcNow.AddYears(-20)
        };

        // assume the list already has one user with the given email
        _mockUnitOfWork.Setup(
            uow => uow.UserRepo.GetQuery(It.IsAny<Expression<Func<User, bool>>>()))
            .Returns(
                new List<User> {
                    new() {  UserName = "User 1",
                            Email = "existinguser@example.com",
                            FirstName = "New",
                            LastName = "User",
                            DateOfBirth = DateTime.UtcNow.AddYears(-20),
                            IsActive = true
                        }
                }
            .AsQueryable());

        _mockUnitOfWork.Setup(uow => uow.UserRepo.Add(It.IsAny<User>())).Returns(false);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.False);
    }
}
