using Fletchling.Api.Controllers;
using Moq;
using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels.Requests;
using Xunit;

namespace Fletchling.Api.Tests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public async Task TestAddUser_ReturnsSuccess()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(x => x.AddUserAsync(It.IsAny<AddUserRequest>())).Returns(Task.CompletedTask);
            var controller = new UserController(userServiceMock.Object);

            // Act
            await controller.AddUser(new AddUserRequest());

            // Assert
            userServiceMock.Verify(x => x.AddUserAsync(It.IsAny<AddUserRequest>()), Times.Once);
        }
    }
}
