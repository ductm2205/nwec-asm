using business.Commands;
using business.Handlers.Users;
using core.Models.Responses;
using data.Infrastructures;
using models.Auth;
using Moq;

namespace test.business.handlers.users;

[TestFixture]
public class GetAllHandlerTest
{
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private GetAllHandler _handler;

    [SetUp]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new GetAllHandler(_mockUnitOfWork.Object);
    }

    [Test]
    public async Task GetAllHandler_Should_Return_All_Users()
    {
        // Arrange
        var users = new List<User>
            {
                new() {  UserName = "User 1",
                            Email = "user1@example.com",
                            FirstName = "John",
                            LastName = "Doe",
                            DateOfBirth = DateTime.UtcNow.AddYears(-20),
                            IsActive = true
                        },
                new() {  UserName = "User 2",
                            Email = "user2@example.com",
                            FirstName = "Jane",
                            LastName = "Doe",
                            DateOfBirth = DateTime.UtcNow.AddYears(-20),
                            IsActive = true
                        }
            };

        _mockUnitOfWork.Setup(uow => uow.UserRepo.GetAllAsync()).ReturnsAsync(users);

        var command = new GetAllCommand<UserResponse>();

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.That(result.Items, Has.Length.EqualTo(2));
        Assert.Multiple(() =>
        {
            Assert.That(result.Items[0].FirstName, Is.EqualTo("John"));
            Assert.That(result.Items[1].FirstName, Is.EqualTo("Jane"));
        });
    }
}
