using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.Core;

namespace Tests.Shared.Mocks.Loggers
{
    public class LoggerMock<T>
    {
        private ILogger<T> _logger;

        private LoggerMock()
        {
            _logger = Substitute.For<ILogger<T>>();
        }

        public static LoggerMock<T> Instance() => new LoggerMock<T>();

        public LoggerMock<T> VerifyLogError(int callsCount = 1)
        {
            var @params = GetLogErrorParameters();

            _logger.Received(callsCount).Log(@params.Item1, @params.Item2, @params.Item3, @params.Item4, @params.Item5);

            return this;
        }

        public LoggerMock<T> SetLogErrorCallBack(Action<CallInfo> callBack)
        {
            var @params = GetLogErrorParameters();

            _logger.When(a => a.Log(@params.Item1, @params.Item2, @params.Item3, @params.Item4, @params.Item5))
                   .Do(callBack);

            return this;
        }

        public ILogger<T> Build() => _logger;

        private (LogLevel, EventId, Arg.AnyType, Exception?, Func<Arg.AnyType, Exception?, string>) GetLogErrorParameters()
            => (LogLevel.Error, Arg.Any<EventId>(), Arg.Any<Arg.AnyType>(), Arg.Any<Exception>(), Arg.Any<Func<Arg.AnyType, Exception?, string>>());
    }
}
