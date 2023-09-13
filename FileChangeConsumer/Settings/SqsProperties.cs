namespace FileChangeConsumer.Settings
{
    public record SqsProperties
    {
        public string QueueName { get; init; } = null!;
    }
}
