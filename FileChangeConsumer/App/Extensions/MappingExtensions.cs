using FileChangeConsumer.App.Messages;
using FileChangeConsumer.Domain.Entities;

namespace FileChangeConsumer.App.Extensions
{
    public static class MappingExtensions
    {
        public static DomainFile MapToDomainFile(this FileUpdatedNotification notification)
        {
            return new DomainFile(notification.fileName, notification.fileSize);
        }
    }
}
