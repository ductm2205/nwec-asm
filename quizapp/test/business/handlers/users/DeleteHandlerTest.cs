using business.Commands.Users;
using business.Handlers.Users;
using data.Infrastructures;
using models.Auth;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace test.business.handlers.users;

[TestFixture]
public class DeleteHandlerTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private DeleteHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new DeleteHandler(_mockUnitOfWork.Object);
    }

    [Test]
    public async Task DeleteHandler_Should_Delete_User_When_User_Exists()
    {
        var userId = Guid.NewGuid();
        User user = new()
        {
            UserName = "User 1",
            Email = "user1@example.com",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = DateTime.UtcNow.AddYears(-20),
            IsActive = true
        };

        _mockUnitOfWork.Setup(uow => uow.UserRepo.GetByIdAsync(userId)).ReturnsAsync(user);
        _mockUnitOfWork.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

        var deleteCommand = new DeleteCommand { Id = userId };

        var result = await _handler.Handle(deleteCommand, CancellationToken.None);

        Assert.That(result, Is.True);
    }

    [Test]
    public async Task DeleteHandler_Should_Return_False_When_User_Does_Not_Exist()
    {
        var userId = Guid.NewGuid();

        _mockUnitOfWork.Setup(uow => uow.UserRepo.GetByIdAsync(userId)).ReturnsAsync((User?)null);

        var deleteCommand = new DeleteCommand { Id = userId };

        var result = await _handler.Handle(deleteCommand, CancellationToken.None);

        Assert.That(result, Is.False);
    }
}
