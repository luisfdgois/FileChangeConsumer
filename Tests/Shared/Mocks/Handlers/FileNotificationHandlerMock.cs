using FileChangeConsumer.App.Handlers;
using FileChangeConsumer.App.Messages;
using NSubstitute;

namespace Tests.Shared.Mocks.Handlers
{
    public class FileNotificationHandlerMock
    {
        private IFileNotificationHandler _fileHandler;

        private FileNotificationHandlerMock()
        {
            _fileHandler = Substitute.For<IFileNotificationHandler>();
        }

        public static FileNotificationHandlerMock Instance() => new FileNotificationHandlerMock();

        public FileNotificationHandlerMock VerifyHandle(int callsCount = 1)
        {
            _fileHandler.Received(callsCount).Handle(Arg.Any<FileUpdatedNotification>(), Arg.Any<CancellationToken>());

            return this;
        }

        public IFileNotificationHandler Build() => _fileHandler;
    }
}
