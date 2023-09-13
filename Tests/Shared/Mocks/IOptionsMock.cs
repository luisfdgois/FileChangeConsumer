using Microsoft.Extensions.Options;
using NSubstitute;

namespace Tests.Shared.Mocks
{
    public class IOptionsMock<T> where T : class
    {
        private IOptions<T> _options;

        public IOptionsMock()
        {
            _options = Substitute.For<IOptions<T>>();
        }

        public static IOptionsMock<T> Instance() => new IOptionsMock<T>();

        public IOptionsMock<T> SetValue(T value)
        {
            _options.Value.Returns(value);

            return this;
        }

        public IOptions<T> Build() => _options;
    }
}
