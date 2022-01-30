using System;
using System.Collections.Generic;
using System.Security.Claims;
using Fletchling.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Tweetinvi.Models;
using UnitTests.Fletchling.Application.Mocks;
using Xunit;

namespace UnitTests.Fletchling.Application.Services
{
    public class TwitterClientFactoryTests : IClassFixture<TwitterClientFactoryMock>
    {
        private readonly TwitterClientFactoryMock _twitterClientFactoryMock;

        public TwitterClientFactoryTests(TwitterClientFactoryMock twitterClientFactoryMock)
        {
            _twitterClientFactoryMock = twitterClientFactoryMock;
        }

        [Fact]
        public void Create_WhenUserFound_ReturnsTwitterClientWithUserCredentials()
        {
            // Arrange
            var defaultCredentials = new TwitterCredentials()
            {
                ConsumerKey = "defaultconsumerkey",
                ConsumerSecret = "defaultconsumersecret",
                AccessToken = "defaultaccesstoken",
                AccessTokenSecret = "defaultaccesstokensecret"
            };
            var service = _twitterClientFactoryMock.CreateServiceMock(defaultCredentials);

            var claims = new List<Claim>()
            {
                new Claim("user_id", "test_user")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext()
            {
                User = claimsPrincipal
            };

            var user = new User()
            {
                UID = "test_user",
                AccessToken = "useraccesstoken",
                AccessTokenSecret = "useraccesstokensecret"
            };

            _twitterClientFactoryMock.ContextAccessor.Setup(x => x.HttpContext)
                                     .Returns(httpContext)
                                     .Verifiable();

            _twitterClientFactoryMock.UserRepo.Setup(x => x.GetUserAsync(It.IsAny<string>()))
                                     .ReturnsAsync(user)
                                     .Verifiable();

            // Act
            var twitterClient = service.Create();

            // Assert
            _twitterClientFactoryMock.ContextAccessor.Verify();
            _twitterClientFactoryMock.UserRepo.Verify();
            Assert.Equal(defaultCredentials.ConsumerKey, twitterClient.Credentials.ConsumerKey);
            Assert.Equal(defaultCredentials.ConsumerSecret, twitterClient.Credentials.ConsumerSecret);
            Assert.Equal(user.AccessToken, twitterClient.Credentials.AccessToken);
            Assert.Equal(user.AccessTokenSecret, twitterClient.Credentials.AccessTokenSecret);
        }
        
        [Fact]
        public void Create_WhenHttpContextNotFound_ReturnsTwitterClientWithDefaultCredentials()
        {
            // Arrange
            var credentials = new TwitterCredentials()
            {
                ConsumerKey = "consumerkey",
                ConsumerSecret = "consumersecret",
                AccessToken = "accesstoken",
                AccessTokenSecret = "accesstokensecret"
            };
            var service = _twitterClientFactoryMock.CreateServiceMock(credentials);

            _twitterClientFactoryMock.ContextAccessor.Setup(x => x.HttpContext)
                                     .Returns((HttpContext)null)
                                     .Verifiable();

            // Act
            var twitterClient = service.Create();

            // Assert
            _twitterClientFactoryMock.ContextAccessor.Verify();
            Assert.Equal(credentials.ConsumerKey, twitterClient.Credentials.ConsumerKey);
            Assert.Equal(credentials.ConsumerSecret, twitterClient.Credentials.ConsumerSecret);
            Assert.Equal(credentials.AccessToken, twitterClient.Credentials.AccessToken);
            Assert.Equal(credentials.AccessTokenSecret, twitterClient.Credentials.AccessTokenSecret);
        }

        [Fact]
        public void Create_WhenUserNotFound_ReturnsTwitterClientWithDefaultCredentials()
        {
            // Arrange
            var credentials = new TwitterCredentials()
            {
                ConsumerKey = "consumerkey",
                ConsumerSecret = "consumersecret",
                AccessToken = "accesstoken",
                AccessTokenSecret = "accesstokensecret"
            };
            var service = _twitterClientFactoryMock.CreateServiceMock(credentials);

            var claims = new List<Claim>()
            {
                new Claim("user_id", "test_user")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext()
            {
                User = claimsPrincipal
            };

            _twitterClientFactoryMock.ContextAccessor.Setup(x => x.HttpContext)
                                     .Returns(httpContext)
                                     .Verifiable();

            _twitterClientFactoryMock.UserRepo.Setup(x => x.GetUserAsync(It.IsAny<string>()))
                                     .ReturnsAsync((User)null)
                                     .Verifiable();

            // Act
            var twitterClient = service.Create();

            // Assert
            _twitterClientFactoryMock.ContextAccessor.Verify();
            _twitterClientFactoryMock.UserRepo.Verify();
            Assert.Equal(credentials.ConsumerKey, twitterClient.Credentials.ConsumerKey);
            Assert.Equal(credentials.ConsumerSecret, twitterClient.Credentials.ConsumerSecret);
            Assert.Equal(credentials.AccessToken, twitterClient.Credentials.AccessToken);
            Assert.Equal(credentials.AccessTokenSecret, twitterClient.Credentials.AccessTokenSecret);
        }

        [Fact]
        public void Create_WhenGetUserAsyncHaveProblems_ReturnsTwitterClientWithDefaultCredentials()
        {
            // Arrange
            var credentials = new TwitterCredentials()
            {
                ConsumerKey = "defaultconsumerkey",
                ConsumerSecret = "defaultconsumersecret",
                AccessToken = "defaultaccesstoken",
                AccessTokenSecret = "defaultaccesstokensecret"
            };
            var service = _twitterClientFactoryMock.CreateServiceMock(credentials);

            var claims = new List<Claim>()
            {
                new Claim("user_id", "test_user")
            };
            var identity = new ClaimsIdentity(claims, "TestAuth");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var httpContext = new DefaultHttpContext()
            {
                User = claimsPrincipal
            };

            _twitterClientFactoryMock.ContextAccessor.Setup(x => x.HttpContext)
                                     .Returns(httpContext)
                                     .Verifiable();

            _twitterClientFactoryMock.UserRepo.Setup(x => x.GetUserAsync(It.IsAny<string>()))
                                     .ThrowsAsync(new Exception("test exception"))
                                     .Verifiable();

            // Act
            var twitterClient = service.Create();

            // Assert
            _twitterClientFactoryMock.ContextAccessor.Verify();
            _twitterClientFactoryMock.UserRepo.Verify();
            _twitterClientFactoryMock.Logger.VerifyLogging(LogLevel.Error, 
                "Error while getting authenticated user's Twitter credentials.");
            Assert.Equal(credentials.ConsumerKey, twitterClient.Credentials.ConsumerKey);
            Assert.Equal(credentials.ConsumerSecret, twitterClient.Credentials.ConsumerSecret);
            Assert.Equal(credentials.AccessToken, twitterClient.Credentials.AccessToken);
            Assert.Equal(credentials.AccessTokenSecret, twitterClient.Credentials.AccessTokenSecret);
        }
    }
}