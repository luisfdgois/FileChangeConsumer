using FileChangeConsumer.Domain.Entities;
using FileChangeConsumer.Domain.Exceptions;
using FluentAssertions;

namespace Tests.Domain
{
    public class DomainFileTests
    {
        [Fact]
        public void WhenTheConstructorIsCalled_ShouldSetPropertiesWithCorrespondingParameters()
        {
            var name = "products_file.txt";
            var size = 150;

            var domainFile = BuildFakeObject(name, size);

            domainFile.Name.Should().Be(name);
            domainFile.Size.Should().Be(size);
        }

        [Fact]
        public void WhenAnObjectIsCreated_LastModifiedPropertyShouldNotBeEmpty()
        {
            var domainFile = BuildFakeObject();

            var emptyDate = new DateTime();

            domainFile.LastModified.Should().NotBe(emptyDate);
        }

        [Fact]
        public void WhenUpdatingProperties_AndNotificationDateIsOlderThenLastModified_ShouldThrowInvalidFileModificationException()
        {
            var domainFile = BuildFakeObject();

            var newName = "products_updated_file.txt";
            var newSize = 300;
            var notificationDate = (domainFile.LastModified).AddSeconds(-1);

            var func = () => { domainFile.Update(newName, newSize, notificationDate); };

            func.Should().Throw<InvalidFileModificationException>();
        }

        [Fact]
        public void WhenUpdatingProperties_AndExceptionIsThrown_ShouldNotUpdateProperties()
        {
            var domainFile = BuildFakeObject();

            var newName = "products_updated_file.txt";
            var newSize = 300;

            var lastModified = domainFile.LastModified;
            var notificationDate = lastModified.AddSeconds(-1);

            try
            {
                domainFile.Update(newName, newSize, notificationDate);
            }
            catch (Exception) { }

            domainFile.Name.Should().NotBe(newName);
            domainFile.Size.Should().NotBe(newSize);
            domainFile.LastModified.Should().Be(lastModified);
        }

        [Fact]
        public void WhenUpdatingProperties_AndConditioIsValid_ShouldUpdateProperties()
        {
            var domainFile = BuildFakeObject();

            var newName = "products_updated_file.txt";
            var newSize = 300;

            var lastModified = domainFile.LastModified;
            var notificationDate = lastModified.AddSeconds(1);

            domainFile.Update(newName, newSize, notificationDate);

            domainFile.Name.Should().Be(newName);
            domainFile.Size.Should().Be(newSize);
            domainFile.LastModified.Should().NotBe(lastModified);
        }

        private DomainFile BuildFakeObject(string? name = default, long? size = default)
            => new DomainFile(name ?? "file_name.txt", size ?? 100);
    }
}
