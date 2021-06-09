using Microsoft.AspNetCore.Http;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Twitter.Services
{
    public class TwitterClientFactory : ITwitterClientFactory
    {
        private readonly TwitterCredentials _credentials;
        private readonly IHttpContextAccessor _contextAccessor;

        public TwitterClientFactory(TwitterCredentials credentials, IHttpContextAccessor contextAccessor)
        {
            _credentials = credentials;
            _contextAccessor = contextAccessor;
        }

        public TwitterClient Create()
        {
            var credentials = new TwitterCredentials(_credentials);

            // Replace with authenticated user's Twitter credentials
            if (_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                credentials.AccessToken = "test";
                credentials.AccessTokenSecret = "test2";
            }

            return new TwitterClient(credentials);
        }
    }
}
