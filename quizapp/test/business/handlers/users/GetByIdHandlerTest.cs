using business.Commands;
using business.Commands.Users;
using business.Handlers.Users;
using core.Exceptions;
using core.Models.Responses;
using data.Infrastructures;
using models.Auth;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace test.business.handlers.users;

[TestFixture]
public class GetByIdHandlerTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private GetByIdHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new GetByIdHandler(_mockUnitOfWork.Object);
    }

    [Test]
    public async Task GetByIdHandler_Should_Return_User_When_User_Exists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new User
        {
            UserName = "User 1",
            Email = "user1@example.com",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.UtcNow.AddYears(-20),
            IsActive = true
        };
        _mockUnitOfWork.Setup(uow => uow.UserRepo.GetByIdAsync(userId)).ReturnsAsync(user);

        var command = new GetByIdCommand<UserResponse> { Id = userId };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.FirstName, Is.EqualTo("John"));
    }

    [Test]
    public Task GetByIdHandler_Should_Return_Null_When_User_Does_Not_Exist()
    {
        var userId = Guid.NewGuid();
        _mockUnitOfWork.Setup(uow => uow.UserRepo.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        var command = new GetByIdCommand<UserResponse> { Id = userId };

        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
        return Task.CompletedTask;
    }
}
