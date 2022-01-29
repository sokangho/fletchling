using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog;

namespace UnitTests.Fletchling.Application.Services
{
    public class TimelineServiceMock
    {
        internal Mock<ITimelineRepository> TimelineRepoMock;
        internal Mock<ILogger<TimelineService>> Logger;
        
        public TimelineService CreateServiceMock()
        {
            TimelineRepoMock = new Mock<ITimelineRepository>();
            Logger = new Mock<ILogger<TimelineService>>();

            var service = new TimelineService(TimelineRepoMock.Object, Logger.Object);
            return service;
        }
    }
}