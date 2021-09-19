using Tweetinvi;

namespace Fletchling.Twitter.Services
{
    public interface ITwitterClientFactory
    {
        TwitterClient Create();
    }
}
