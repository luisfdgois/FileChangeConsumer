using Amazon.SQS.Model;
using FileChangeConsumer.App.Messages;
using System.Text.Json;

namespace FileChangeConsumer.App.Extensions
{
    public static class MessageExtensions
    {
        public static Message GetNextMessage(this List<Message> messages)
        {
            return messages[0];
        }

        public static FileUpdatedNotification ConvertToDomainNotification(this Message message)
        {
            return JsonSerializer.Deserialize<FileUpdatedNotification>(message.Body)!;
        }
    }
}
