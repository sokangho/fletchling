using Tweetinvi;

namespace Fletchling.Application.Interfaces.Services
{
    public interface ITwitterClientFactory
    {
        TwitterClient Create();
    }
}