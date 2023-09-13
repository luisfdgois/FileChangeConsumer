using FileChangeConsumer.App.Handlers;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Tests.Shared.Mocks.Services
{
    public class ServiceProviderMock
    {
        private IServiceProvider _serviceProvider;

        private ServiceProviderMock()
        {
            _serviceProvider = Substitute.For<IServiceProvider>();
        }

        public static ServiceProviderMock Instance() => new ServiceProviderMock();

        public ServiceProviderMock SetCreateScope<T>(T service)
        {
            _serviceProvider.GetService(typeof(T)).Returns(service);  

            var serviceScope = Substitute.For<IServiceScope>();
            serviceScope.ServiceProvider.Returns(_serviceProvider);

            var serviceScopeFactory = Substitute.For<IServiceScopeFactory>();
            serviceScopeFactory.CreateScope().Returns(serviceScope);

            _serviceProvider.GetService(typeof(IServiceScopeFactory)).Returns(serviceScopeFactory);

            return this;
        }

        public IServiceProvider Build() => _serviceProvider;
    }
}
