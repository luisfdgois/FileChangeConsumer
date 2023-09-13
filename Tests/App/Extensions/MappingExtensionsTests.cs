using FileChangeConsumer.App.Extensions;
using FileChangeConsumer.Domain.Entities;
using FluentAssertions;
using Tests.Shared.Builders.Messages;

namespace Tests.App.Extensions
{
    public class MappingExtensionsTests
    {
        [Fact]
        public void WhenMapToDomainFileIsCalled_ShouldReturnADomainFileObject()
        {
            var notification = FileUpdatedNotificationBuilder.Map().Generate();

            var domainFile = notification.MapToDomainFile();

            domainFile.Should().NotBeNull();
            domainFile.Should().BeOfType<DomainFile>();
        }

        [Fact]
        public void WhenMapToDomainFileIsCalled_ShouldMapParametersToCorrespondingProperties()
        {
            var notification = FileUpdatedNotificationBuilder.Map().Generate();

            var domainFile = notification.MapToDomainFile();

            domainFile.Name.Should().Be(notification.fileName);
            domainFile.Size.Should().Be(notification.fileSize);
        }
    }
}
