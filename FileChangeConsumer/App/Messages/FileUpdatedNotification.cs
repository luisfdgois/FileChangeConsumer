using System.Text.Json;

namespace FileChangeConsumer.App.Messages
{
    public record FileUpdatedNotification
    {
        public string fileName { get; init; }
        public long fileSize { get; init; }
        public DateTime date { get; init; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
