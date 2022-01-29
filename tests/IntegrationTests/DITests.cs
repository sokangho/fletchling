using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Program = Fletchling.Api.Program;

namespace IntegrationTests
{
    public class DITests
    {
        [Fact]
        public void AllDependenciesAreRegistered()
        {
            var services = Program.CreateHostBuilder()
                                  .Build()
                                  .Services;

            services.GetRequiredService<ITimelineService>();
            services.GetRequiredService<ITwitterClientFactory>();
            services.GetRequiredService<ITwitterService>();
            services.GetRequiredService<IUserService>();

            services.GetRequiredService<ITimelineRepository>();
            services.GetRequiredService<IUserRepository>();
        }
    }
}