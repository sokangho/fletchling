using Tweetinvi;

namespace Fletchling.Business.Contracts
{
    public interface ITwitterClientFactory
    {
        TwitterClient Create();
    }
}
