using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Tweetinvi.Models;

namespace UnitTests.Fletchling.Application.Mocks
{
    public class TwitterClientFactoryMock
    {
        internal Mock<IHttpContextAccessor> ContextAccessor { get; private set; }
        internal Mock<IUserRepository> UserRepo { get; private set; }
        internal Mock<ILogger<TwitterClientFactory>> Logger { get; private set; }

        internal TwitterClientFactory CreateServiceMock(TwitterCredentials credentials)
        {
            ContextAccessor = new Mock<IHttpContextAccessor>();
            UserRepo = new Mock<IUserRepository>();
            Logger = new Mock<ILogger<TwitterClientFactory>>();

            return new TwitterClientFactory(credentials, ContextAccessor.Object, UserRepo.Object, Logger.Object);
        }
    }
}