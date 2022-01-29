using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Application.Exceptions;
using Fletchling.Domain.Entities;
using Moq;
using Xunit;

namespace UnitTests.Fletchling.Application.Services
{
    public class TimelineServiceTests : IClassFixture<TimelineServiceMock>
    {
        private readonly TimelineServiceMock _timelineServiceMock;

        public TimelineServiceTests(TimelineServiceMock timelineServiceMock)
        {
            _timelineServiceMock = timelineServiceMock;
        }

        [Fact]
        public async Task GetTimelineGroupByNameAsync_WhenFound_ReturnsTimelineGroup()
        {
            // Arrange
            var service = _timelineServiceMock.CreateServiceMock();

            var timelineGroup = new TimelineGroup()
            {
                Reference = default,
                Name = "test",
                Timelines = new List<string>()
            };

            _timelineServiceMock.TimelineRepoMock.Setup(x =>
                                    x.GetTimelineGroupByNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                                .ReturnsAsync(timelineGroup)
                                .Verifiable();

            // Act
            var actual = await service.GetTimelineGroupByNameAsync("123", "test");

            // Assert
            Assert.Equal(timelineGroup, actual);
            _timelineServiceMock.TimelineRepoMock.Verify();
        }

        [Fact]
        public async Task SetTimelinesInGroupAsync_WhenTimelineGroupNotFound_ThrowsDataNotFoundException()
        {
            // Arrange
            var service = _timelineServiceMock.CreateServiceMock();
            _timelineServiceMock.TimelineRepoMock.Setup(x =>
                                    x.GetTimelineGroupByNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                                .ReturnsAsync((TimelineGroup)null)
                                .Verifiable();

            var uid = "123";
            var timelines = new List<string>();
            var groupName = "test";

            // Act
            Task Act() => service.SetTimelinesInGroupAsync(uid, timelines, groupName);

            // Assert
            var exception = await Assert.ThrowsAsync<DataNotFoundException>(Act);
            Assert.Equal($"Timeline group with name: '{groupName}' does not exist.", exception.Message);
            _timelineServiceMock.TimelineRepoMock.Verify();
        }

        [Fact]
        public async Task SetTimelinesInGroupAsync_WhenTimelineGroupFound_SetTimelinesInGroupAsync()
        {
            // Arrange
            var service = _timelineServiceMock.CreateServiceMock();

            var timelineGroup = new TimelineGroup()
            {
                Reference = default,
                Name = "test",
                Timelines = new List<string>()
            };

            _timelineServiceMock.TimelineRepoMock.Setup(x =>
                                    x.GetTimelineGroupByNameAsync(It.IsAny<string>(), It.IsAny<string>()))
                                .ReturnsAsync(timelineGroup)
                                .Verifiable();
            _timelineServiceMock.TimelineRepoMock.Setup(x =>
                                    x.SetTimelinesInGroupAsync(It.IsAny<TimelineGroup>(), It.IsAny<List<string>>()))
                                .Verifiable();

            var uid = "123";
            var timelines = new List<string>();
            var groupName = "test";

            // Act
            await service.SetTimelinesInGroupAsync(uid, timelines, groupName);

            // Assert
            _timelineServiceMock.TimelineRepoMock.Verify();
        }
    }
}