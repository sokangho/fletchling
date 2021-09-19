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
            var twitterCredentials = new TwitterCredentials();
            config.GetSection("TwitterCredentials").Bind(twitterCredentials);
            services.AddSingleton(twitterCredentials);

            services.AddHttpContextAccessor();
            services.AddScoped<ITwitterClientFactory, TwitterClientFactory>();
            services.AddScoped<ITwitterClient>(x => x.GetService<ITwitterClientFactory>().Create());
            
            services.AddTransient<ITwitterService, TwitterService>();

            return services;
        }
    }
}
