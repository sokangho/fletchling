using Fletchling.Business;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fletchling.Api
{
    public static class Bindings
    {
        public static IServiceCollection RegisterBindings(this IServiceCollection services, IConfiguration config)
        {
            services.RegisterBusinessServices(config);            

            return services;
        }
    }
}
