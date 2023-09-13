namespace FileChangeConsumer.Settings
{
    public record SqsCredentials
    {
        public string Region { get; init; } = null!;
        public string AccessKey { get; init; } = null!;
        public string SecretKey { get; init; } = null!;

        public string ServiceURL { get { return $"https://sqs.{Region}.amazonaws.com"; } }
    }
}
