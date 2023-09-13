using FileChangeConsumer.App.Handlers;
using FluentAssertions;
using Tests.Shared.Builders.Entities;
using Tests.Shared.Builders.Messages;
using Tests.Shared.Mocks.Logs;
using Tests.Shared.Mocks.Repositories;

namespace Tests.App.Handlers
{
    public class FileNotificationHandlerTests
    {
        [Fact]
        public async Task WhenDomainFileDoesNotExist_ShouldAddANewOne()
        {
            var logger = LoggerMock<FileNotificationHandler>.Instance().Build();
            var repository = DomainFileRepositoryMock.Instance().SetGetByName();

            var handler = new FileNotificationHandler(logger, repository.Build());

            var notification = FileUpdatedNotificationBuilder.Map().Generate();

            await handler.Handle(notification);

            repository.VerifyAdd(callsCount: 1);
        }

        [Fact]
        public async Task WhenDomainFileExists_ShouldUpdateIt()
        {
            var domainFile = DomainFileBuilder.Map().Generate();

            var logger = LoggerMock<FileNotificationHandler>.Instance().Build();
            var repository = DomainFileRepositoryMock.Instance().SetGetByName(domainFile).Build();

            var handler = new FileNotificationHandler(logger, repository);

            var notification = FileUpdatedNotificationBuilder.Map(fileName: domainFile.Name).Generate();

            await handler.Handle(notification);

            domainFile.Size.Should().Be(notification.fileSize);
        }

        [Fact]
        public async Task WhenAnExceptionIsThrown_ShouldLogError()
        {
            var domainFile = DomainFileBuilder.Map().Generate();

            var logger = LoggerMock<FileNotificationHandler>.Instance();
            var repository = DomainFileRepositoryMock.Instance().SetGetByName(domainFile).Build();

            var handler = new FileNotificationHandler(logger.Build(), repository);

            var notification = FileUpdatedNotificationBuilder.Map(fileName: domainFile.Name, date: domainFile.LastModified.AddSeconds(-1)).Generate();

            await handler.Handle(notification);

            logger.VerifyLogError(callsCount: 1);
        }

        [Fact]
        public async Task WhenAnExceptionIsThrown_ShouldNotCommitChanges()
        {
            var domainFile = DomainFileBuilder.Map().Generate();

            var logger = LoggerMock<FileNotificationHandler>.Instance().Build();
            var repository = DomainFileRepositoryMock.Instance().SetGetByName(domainFile);

            var handler = new FileNotificationHandler(logger, repository.Build());

            var notification = FileUpdatedNotificationBuilder.Map(fileName: domainFile.Name, date: domainFile.LastModified.AddSeconds(-1)).Generate();

            await handler.Handle(notification);

            repository.VerifyCommitChanges(callsCount: 0);
        }

        [Fact]
        public async Task WhenActionIsCompleted_ShouldCommitChanges()
        {
            var domainFile = DomainFileBuilder.Map().Generate();

            var logger = LoggerMock<FileNotificationHandler>.Instance().Build();
            var repository = DomainFileRepositoryMock.Instance().SetGetByName(domainFile);

            var handler = new FileNotificationHandler(logger, repository.Build());

            var notification = FileUpdatedNotificationBuilder.Map(fileName: domainFile.Name).Generate();

            await handler.Handle(notification);

            repository.VerifyCommitChanges(callsCount: 1);
        }
    }
}
