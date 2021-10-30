using Fletchling.Application;
using Fletchling.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fletchling.Api
{
    public static class Bindings
    {
        public static IServiceCollection RegisterBindings(this IServiceCollection services, IConfiguration config)
        {
            services.AddApplicationServices(config);
            services.AddDataServices(config);

            return services;
        }
    }
}
