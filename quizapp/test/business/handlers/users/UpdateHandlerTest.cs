using business.Commands.Users;
using business.Handlers.Users;
using core.Exceptions;
using data.Infrastructures;
using models.Auth;
using Moq;

namespace test.business.handlers.users;

[TestFixture]
public class UpdateHandlerTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private UpdateHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new UpdateHandler(_mockUnitOfWork.Object);
    }

    [Test]
    public async Task UpdateHandler_Should_Update_User_When_User_Exists()
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            UserName = "OldUser",
            Email = "old@example.com",
            FirstName = "Old",
            LastName = "User",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            IsActive = false
        };

        var updateCommand = new UpdateCommand
        {
            Id = userId,
            UserName = "NewUser",
            Email = "new@example.com",
            FirstName = "New",
            LastName = "User",
            Password = "test",
            DateOfBirth = DateTime.UtcNow.AddYears(-25),
            IsActive = true
        };

        _mockUnitOfWork.Setup(uow => uow.UserRepo.GetByIdAsync(userId)).ReturnsAsync(user);
        _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var result = await _handler.Handle(updateCommand, CancellationToken.None);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.True);
            Assert.That(user.UserName, Is.EqualTo("NewUser"));
            Assert.That(user.Email, Is.EqualTo("new@example.com"));
            Assert.That(user.FirstName, Is.EqualTo("New"));
            Assert.That(user.LastName, Is.EqualTo("User"));
            Assert.That(user.IsActive, Is.True);
        });
    }

    [Test]
    public void UpdateHandler_Should_Throw_Exception_When_User_Does_Not_Exist()
    {
        var userId = Guid.NewGuid();
        var updateCommand = new UpdateCommand
        {
            Id = userId,
            UserName = "NewUser",
            Email = "new@example.com",
            FirstName = "New",
            LastName = "User",
            Password = "test",
            DateOfBirth = DateTime.UtcNow.AddYears(-25),
            IsActive = true
        };

        _mockUnitOfWork.Setup(uow => uow.UserRepo.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(updateCommand, CancellationToken.None));
    }
}
