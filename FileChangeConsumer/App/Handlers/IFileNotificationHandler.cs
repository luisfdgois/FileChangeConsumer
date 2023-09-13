using FileChangeConsumer.App.Messages;

namespace FileChangeConsumer.App.Handlers
{
    public interface IFileNotificationHandler
    {
        Task Handle(FileUpdatedNotification notification, CancellationToken cancellationToken = default);
    }
}
