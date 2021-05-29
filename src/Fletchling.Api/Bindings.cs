using Fletchling.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fletchling.Api
{
    public static class Bindings
    {
        public static IServiceCollection RegisterBindings(this IServiceCollection services)
        {
            services.RegisterDatabase();

            return services;
        }
    }
}
