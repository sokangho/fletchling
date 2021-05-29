using Fletchling.Data;
using Fletchling.Twitter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fletchling.Api
{
    public static class Bindings
    {
        public static IServiceCollection RegisterBindings(this IServiceCollection services, IConfiguration config)
        {
            services.RegisterDatabase();
            services.RegisterTwitterServices(config);

            return services;
        }
    }
}
