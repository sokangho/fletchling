using Fletchling.Business.Contracts;
using Fletchling.Business.Services;
using Fletchling.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Business
{
    public static class Bindings
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration config)
        {
            var twitterCredentials = new TwitterCredentials();
            config.GetSection("TwitterCredentials").Bind(twitterCredentials);
            services.AddSingleton(twitterCredentials);

            services.AddHttpContextAccessor();
            services.AddScoped<ITwitterClientFactory, TwitterClientFactory>();
            services.AddScoped<ITwitterClient>(x => x.GetService<ITwitterClientFactory>().Create());
            
            services.AddTransient<ITwitterService, TwitterService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITimelineService, TimelineService>();

            services.RegisterDataServices(config);

            return services;
        }
    }
}
