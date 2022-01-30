using System.Linq;
using System.Threading.Tasks;
using Fletchling.Application.Exceptions;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Application.Services
{
    public class TwitterClientFactory : ITwitterClientFactory
    {
        private readonly TwitterCredentials _credentials;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepo;

        public TwitterClientFactory(TwitterCredentials credentials, IHttpContextAccessor contextAccessor,
            IUserRepository userRepo)
        {
            _credentials = credentials;
            _contextAccessor = contextAccessor;
            _userRepo = userRepo;
        }

        public TwitterClient Create()
        {
            if (_contextAccessor?.HttpContext == null)
            {
                return null;
            }

            var userContext = _contextAccessor.HttpContext.User;
            var credentials = new TwitterCredentials(_credentials);

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
                    credentials.AccessToken = user.AccessToken;
                    credentials.AccessTokenSecret = user.AccessTokenSecret;
                }
                catch (DataNotFoundException)
                {
                    // swallow user not found exception and use the default twitter credential
                }
            }

            return new TwitterClient(credentials);
        }
    }
}