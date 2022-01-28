using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Services;
using Fletchling.Domain.ApiModels.Requests;
using Moq;
using Xunit;

namespace UnitTests.Fletchling.Application.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task AddUserAsync_Successful()
        {
            // Arrange
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(x => x.AddUserAsync(It.IsAny<AddUserRequest>()))
                        .Returns(Task.CompletedTask);

            var service = new UserService(userRepoMock.Object);
            
            // Act
            await service.AddUserAsync(new AddUserRequest());
            
            // Assert
            userRepoMock.Verify(x => x.AddUserAsync(It.IsAny<AddUserRequest>()), Times.Once);
        }
    }
}