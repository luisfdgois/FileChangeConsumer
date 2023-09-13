using Bogus;
using FileChangeConsumer.App.Messages;

namespace Tests.Shared.Builders.Messages
{
    public static class FileUpdatedNotificationBuilder
    {
        public static Faker<FileUpdatedNotification> Map(string? fileName = default, long? fileSize = default, DateTime? date = default) =>
            new Faker<FileUpdatedNotification>().CustomInstantiator(f =>
            new FileUpdatedNotification
            {
                fileName = fileName ?? f.System.FileName(),
                fileSize = fileSize ?? f.Random.Long(min: 1, max: 1000),
                date = date ?? f.Date.Future()
            });
    }
}
