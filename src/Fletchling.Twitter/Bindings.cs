using Fletchling.Twitter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Twitter
{
    public static class Bindings
    {
        public static IServiceCollection RegisterTwitterServices(this IServiceCollection services, IConfiguration config)
        {
            var twitterCreds = new TwitterCredentials(config["TwitterCredentials:ConsumerKey"], config["TwitterCredentials:ConsumerSecretKey"]
                ,config["TwitterCredentials:AccessToken"], config["TwitterCredentials:AccessTokenSecret"]);            
            services.AddScoped<ITwitterClient>(f => new TwitterClient(twitterCreds));
            
            services.AddTransient<ITwitterService, TwitterService>();

            return services;
        }
    }
}
