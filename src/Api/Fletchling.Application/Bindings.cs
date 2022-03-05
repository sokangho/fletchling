using Fletchling.Application.Config;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweetinvi;
using Tweetinvi.Models;

namespace Fletchling.Application
{
    public static class Bindings
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            var twitterCredentials = new TwitterCredentials();
            config.GetSection("TwitterCredentials").Bind(twitterCredentials);
            services.AddSingleton(twitterCredentials);

            services.Configure<JwtConfig>(options => config.GetSection(nameof(JwtConfig)).Bind(options));

            services.AddHttpContextAccessor();
            services.AddScoped<ITwitterClientFactory, TwitterClientFactory>();
            services.AddScoped<ITwitterClient>(x => x.GetService<ITwitterClientFactory>()!.Create());

            services.AddTransient<IJwtService, JwtService>();
            services.AddTransient<ITwitterService, TwitterService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITimelineService, TimelineService>();

            return services;
        }
    }
}