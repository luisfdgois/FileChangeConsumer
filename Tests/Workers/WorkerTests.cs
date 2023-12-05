using FileChangeConsumer;
using FileChangeConsumer.Settings;
using System.Net;
using Tests.Shared.Builders.Messages;
using Tests.Shared.Builders.SQS;
using Tests.Shared.Mocks;
using Tests.Shared.Mocks.Handlers;
using Tests.Shared.Mocks.Loggers;
using Tests.Shared.Mocks.Services;
using Tests.Shared.Mocks.SQS;

namespace Tests.Workers
{
    public class WorkerTests
    {
        [Fact]
        public async Task WhenResponseIsNotValid_ShouldLogError()
        {
            var cancellationToken = new CancellationTokenSource();

            var options = IOptionsMock<SqsProperties>.Instance().SetValue(new SqsProperties { QueueName = "worker_tests" }).Build();
            var queueUrl = GetQueueUrlResponseBuilder.Map().Generate();
            var messageResponse = ReceiveMessageResponseBuilder.Map().WithHttpStatusCode(HttpStatusCode.NotFound).Generate();

            var amazonSQS = AmazonSQSMock.Instance().SetGetQueueUrlAsync(queueUrl).SetReceiveMessageAsync(messageResponse).Build();
            var serviceProvider = ServiceProviderMock.Instance().Build();
            var logger = LoggerMock<Worker>.Instance().SetLogErrorCallBack(_ => cancellationToken.Cancel());

            var worker = new Worker(options, amazonSQS, logger.Build(), serviceProvider);

            await worker.StartAsync(cancellationToken.Token);

            logger.VerifyLogError(callsCount: 1);
        }

        [Fact]
        public async Task WhenMessageBodyIsNotValid_ShouldLogError()
        {
            var cancellationToken = new CancellationTokenSource();

            var options = IOptionsMock<SqsProperties>.Instance().SetValue(new SqsProperties { QueueName = "worker_tests" }).Build();
            var queueUrl = GetQueueUrlResponseBuilder.Map().Generate();
            var messages = MessageBuilder.Map().WithBody("invalid message format.").Generate(1);
            var messageResponse = ReceiveMessageResponseBuilder.Map().WithMessages(messages).Generate();

            var amazonSQS = AmazonSQSMock.Instance().SetGetQueueUrlAsync(queueUrl).SetReceiveMessageAsync(messageResponse).Build();
            var handler = FileNotificationHandlerMock.Instance().Build();
            var serviceProvider = ServiceProviderMock.Instance().SetCreateScope(handler).Build();
            var logger = LoggerMock<Worker>.Instance().SetLogErrorCallBack(_ => cancellationToken.Cancel());

            var worker = new Worker(options, amazonSQS, logger.Build(), serviceProvider);

            await worker.StartAsync(cancellationToken.Token);

            logger.VerifyLogError(callsCount: 1);
        }

        [Fact]
        public async Task WhenMessageBodyIsValid_ShouldHandleNotification()
        {
            var cancellationToken = new CancellationTokenSource();

            var options = IOptionsMock<SqsProperties>.Instance().SetValue(new SqsProperties { QueueName = "worker_tests" }).Build();
            var queueUrl = GetQueueUrlResponseBuilder.Map().Generate();
            var messages = MessageBuilder.Map().Generate(1);
            var messageResponse = ReceiveMessageResponseBuilder.Map().WithMessages(messages).Generate();

            var amazonSQS = AmazonSQSMock.Instance().SetGetQueueUrlAsync(queueUrl).SetReceiveMessageAsync(messageResponse)
                                                    .SetDeleteMessageAsyncCallBack(_ => cancellationToken.Cancel()).Build();
            var handler = FileNotificationHandlerMock.Instance();
            var serviceProvider = ServiceProviderMock.Instance().SetCreateScope(handler.Build()).Build();
            var logger = LoggerMock<Worker>.Instance().SetLogErrorCallBack(_ => cancellationToken.Cancel());

            var worker = new Worker(options, amazonSQS, logger.Build(), serviceProvider);

            await worker.StartAsync(cancellationToken.Token);

            handler.VerifyHandle(callsCount: 1);
        }

        [Fact]
        public async Task WhenMessageIsProcessed_ShouldDeleteItFromQueue()
        {
            var cancellationToken = new CancellationTokenSource();

            var options = IOptionsMock<SqsProperties>.Instance().SetValue(new SqsProperties { QueueName = "worker_tests" }).Build();
            var queueUrl = GetQueueUrlResponseBuilder.Map().Generate();
            var messages = MessageBuilder.Map().Generate(1);
            var messageResponse = ReceiveMessageResponseBuilder.Map().WithMessages(messages).Generate();

            var amazonSQS = AmazonSQSMock.Instance().SetGetQueueUrlAsync(queueUrl).SetReceiveMessageAsync(messageResponse)
                                                    .SetDeleteMessageAsyncCallBack(_ => cancellationToken.Cancel());
            var handler = FileNotificationHandlerMock.Instance().Build();
            var serviceProvider = ServiceProviderMock.Instance().SetCreateScope(handler).Build();
            var logger = LoggerMock<Worker>.Instance().Build();

            var worker = new Worker(options, amazonSQS.Build(), logger, serviceProvider);

            await worker.StartAsync(cancellationToken.Token);

            amazonSQS.VeriryDeleteMessageAsync(callsCount: 1);
        }
    }
}
