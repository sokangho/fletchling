using Fletchling.Twitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweetinvi;

namespace Fletchling.Twitter.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly ITwitterClient _client;

        public TwitterService(ITwitterClient client)
        {
            _client = client;
        }

        public async Task<List<TwitterUser>> SearchUsersAsync(string username)
        {
            var res = await _client.Search.SearchUsersAsync(username);

            var users = new List<TwitterUser>();
            foreach (var user in res)
            {
                users.Add(new TwitterUser
                {
                    Id = user.Id,
                    Username = user.ScreenName,
                    DisplayName = user.Name,
                    Verified = user.Verified,                    
                    ProfileImageUrl = user.ProfileImageUrl
                });
            }
            
            return users;
        }
    }
}
