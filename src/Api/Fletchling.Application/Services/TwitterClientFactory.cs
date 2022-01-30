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
            // Default Twitter credentials
            var credentials = new TwitterCredentials(_credentials);

            if (_contextAccessor?.HttpContext == null)
            {
                // Use default twitter credentials
                return new TwitterClient(credentials);
            }

            var userContext = _contextAccessor.HttpContext.User;

            // Replace with authenticated user's Twitter credentials
            if (userContext.Identity is { IsAuthenticated: true })
            {
                var uid = userContext.Claims.Where(x => x.Type == "user_id")
                                     .Select(x => x.Value)
                                     .FirstOrDefault();

                try
                {
                    var task = Task.Run<User>(async () => await _userRepo.GetUserAsync(uid));
                    var user = task.Result;

                    if (user != null)
                    {
                        credentials.AccessToken = user.AccessToken;
                        credentials.AccessTokenSecret = user.AccessTokenSecret;
                    }
                }
                catch (Exception ex)
                {
                    // swallow all exceptions and use the default twitter credentials
                    _logger.LogError(ex, "Error while getting authenticated user's Twitter credentials.");
                }
            }

            return new TwitterClient(credentials);
        }
    }
}