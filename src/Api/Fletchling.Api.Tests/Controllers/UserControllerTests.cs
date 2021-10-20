using Fletchling.Api.Controllers;
using Fletchling.Business.Contracts;
using Fletchling.Data.Models;
using Moq;
using System.Threading.Tasks;
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
            userServiceMock.Setup(x => x.AddUserAsync(It.IsAny<User>())).Returns(Task.CompletedTask);
            var controller = new UserController(userServiceMock.Object);

            // Act
            await controller.AddUser(new User());

            // Assert
            userServiceMock.Verify(x => x.AddUserAsync(It.IsAny<User>()), Times.Never);
        }

        //[Fact]
        //public async Task TestAddUser_ThrowsBusinessException_Returns
    }
}
