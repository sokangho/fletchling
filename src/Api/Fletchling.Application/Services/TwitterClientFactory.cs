using System;
using System.Linq;
using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Application.Services
{
    public class TwitterClientFactory : ITwitterClientFactory
    {
        private readonly TwitterCredentials _credentials;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepo;
        private readonly ILogger<TwitterClientFactory> _logger;

        public TwitterClientFactory(TwitterCredentials credentials, IHttpContextAccessor contextAccessor,
            IUserRepository userRepo, ILogger<TwitterClientFactory> logger)
        {
            _credentials = credentials;
            _contextAccessor = contextAccessor;
            _userRepo = userRepo;
            _logger = logger;
        }

        public TwitterClient Create()
        {
            if (_contextAccessor?.HttpContext == null || !_contextAccessor.HttpContext.User.Identity!.IsAuthenticated)
            {
                return null;
            }
            
            var userContext = _contextAccessor.HttpContext.User;
            
            // Get AccessToken and RefreshToken from UserContext
            var accessToken = userContext.Claims.Where(x => x.Type == "AccessToken")
                                         .Select(x => x.Value)
                                         .FirstOrDefault();
            var refreshToken = userContext.Claims.Where(x => x.Type == "RefreshToken")
                                          .Select(x => x.Value)
                                          .FirstOrDefault();

            var credentials = new TwitterCredentials(_credentials)
            {
                AccessToken = accessToken,
                AccessTokenSecret = refreshToken
            };

            return new TwitterClient(credentials);
        }
    }
}