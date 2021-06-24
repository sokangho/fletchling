using Fletchling.Data.Exceptions;
using Fletchling.Data.Repositories;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Twitter.Services
{
    public class TwitterClientFactory : ITwitterClientFactory
    {
        private readonly TwitterCredentials _credentials;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepo;

        public TwitterClientFactory(TwitterCredentials credentials, IHttpContextAccessor contextAccessor, IUserRepository userRepo)
        {
            _credentials = credentials;
            _contextAccessor = contextAccessor;
            _userRepo = userRepo;
        }

        public TwitterClient Create()
        {
            var userContext = _contextAccessor.HttpContext.User;
            var credentials = new TwitterCredentials(_credentials);

            // Replace with authenticated user's Twitter credentials
            if (userContext.Identity.IsAuthenticated)
            {
                var uid = userContext.Claims.Where(x => x.Type == "user_id").Select(x => x.Value).FirstOrDefault();

                try
                {
                    var user = _userRepo.GetUserAsync(uid).GetAwaiter().GetResult();
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
