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
            _logger.Received(callsCount).Log(LogLevel.Error,
                                             Arg.Any<EventId>(),
                                             Arg.Any<Arg.AnyType>(),
                                             Arg.Any<Exception>(),
                                             Arg.Any<Func<Arg.AnyType, Exception?, string>>());

            return this;
        }

        public LoggerMock<T> SetLogErrorCallBack(Action<CallInfo> callBack)
        {
            _logger.When(a => a.Log(LogLevel.Error,
                                    Arg.Any<EventId>(),
                                    Arg.Any<Arg.AnyType>(),
                                    Arg.Any<Exception>(),
                                    Arg.Any<Func<Arg.AnyType, Exception?, string>>()))
                   .Do(callBack);

            return this;
        }

        public ILogger<T> Build() => _logger;
    }
}
