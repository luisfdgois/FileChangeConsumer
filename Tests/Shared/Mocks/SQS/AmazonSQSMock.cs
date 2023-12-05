using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Core;
using Tests.Shared.Mocks.Loggers;

namespace Tests.Shared.Mocks.SQS
{
    public class AmazonSQSMock
    {
        private IAmazonSQS _amazonSQS;

        private AmazonSQSMock()
        {
            _amazonSQS = Substitute.For<IAmazonSQS>();
        }

        public static AmazonSQSMock Instance() => new AmazonSQSMock();

        public AmazonSQSMock SetGetQueueUrlAsync(GetQueueUrlResponse response)
        {
            _amazonSQS.GetQueueUrlAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(response);

            return this;
        }

        public AmazonSQSMock SetReceiveMessageAsync(ReceiveMessageResponse response)
        {
            _amazonSQS.ReceiveMessageAsync(Arg.Any<ReceiveMessageRequest>(), Arg.Any<CancellationToken>()).Returns(response);

            return this;
        }

        public AmazonSQSMock VeriryDeleteMessageAsync(int callsCount = 1)
        {
            _amazonSQS.Received(callsCount).DeleteMessageAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());

            return this;
        }

        public AmazonSQSMock SetDeleteMessageAsyncCallBack(Action<CallInfo> callBack)
        {
            _amazonSQS.When(a => a.DeleteMessageAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())).Do(callBack);

            return this;
        }


        public IAmazonSQS Build() => _amazonSQS;    
    }
}
