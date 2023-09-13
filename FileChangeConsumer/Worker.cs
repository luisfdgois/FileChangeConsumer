using Amazon.SQS;
using Amazon.SQS.Model;
using FileChangeConsumer.App.Extensions;
using FileChangeConsumer.App.Handlers;
using FileChangeConsumer.Settings;
using Microsoft.Extensions.Options;
using System.Net;

namespace FileChangeConsumer
{
    public class Worker : BackgroundService
    {
        private readonly SqsProperties _sqsProperties;
        private readonly IAmazonSQS _amazonSQS;
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        private const int _maxNumberOfMessages = 1;

        public Worker(IOptions<SqsProperties> sqsProperties, IAmazonSQS amazonSQS, ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _sqsProperties = sqsProperties.Value;
            _amazonSQS = amazonSQS;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var queueUrl = await _amazonSQS.GetQueueUrlAsync(_sqsProperties.QueueName, cancellationToken);

            var messageRequest = SetReceiveMessagePriorities(queueUrl.QueueUrl);

            while (!cancellationToken.IsCancellationRequested)
            {
                var response = await _amazonSQS.ReceiveMessageAsync(messageRequest, cancellationToken);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    _logger.LogError($"It could not retrieve messages from the SQS service. StatusCode: {response.HttpStatusCode}");
                    continue;
                }

                if (!response.Messages.Any()) continue;

                var message = response.Messages.GetNextMessage();

                await HandleMessage(message, cancellationToken);

                await _amazonSQS.DeleteMessageAsync(queueUrl.QueueUrl, message.ReceiptHandle, cancellationToken);
            }
        }

        private ReceiveMessageRequest SetReceiveMessagePriorities(string queueUrl) => new ReceiveMessageRequest { QueueUrl = queueUrl, MaxNumberOfMessages = _maxNumberOfMessages };

        private async Task HandleMessage(Message message, CancellationToken cancellationToken)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var notification = message.ConvertToDomainNotification();

                var fileHandler = scope.ServiceProvider.GetRequiredService<IFileNotificationHandler>();

                await fileHandler.Handle(notification, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.GetType().FullName}: {ex.Message} \n{message.Body}");
            }
        }
    }
}