using System.Threading.Tasks;
using Tweetinvi;
using Tweetinvi.Models.V2;

namespace Fletchling.Twitter.Services
{
    public class TwitterSearchUser
    {
        private readonly ITwitterClient _client;

        public TwitterSearchUser(ITwitterClient client)
        {
            _client = client;            
        }

        public async Task<UserV2[]> GetUsersByNameAsync()
        {
            var res = await _client.UsersV2.GetUsersByNameAsync("tweetinviapi");
            return res.Users;
        }
    }
}
