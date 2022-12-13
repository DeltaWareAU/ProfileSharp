using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Mocking.Factory;
using ProfileSharp.Mocking.Scope;
using ProfileSharp.Mocking.Store;
using ProfileSharp.Wrappers;
using System;
using System.Linq;

namespace ProfileSharp.Mocking.Configuration
{
    internal sealed class MockingConfigurationBuilder : IMockingConfigurationBuilder
    {
        public IServiceCollection Services { get; }

        public MockingConfigurationBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public void UseStore<T>() where T : class, IMockDataStore
            => Services.AddScoped<IMockDataStore, T>();

        public void UseStore<T>(Func<IServiceProvider, T> implementationFactory) where T : class, IMockDataStore
            => Services.AddScoped<IMockDataStore, T>(implementationFactory);

        public void Build()
        {
            if (Services.All(s => s.ServiceType != typeof(IMockDataStore)))
            {
                throw new InvalidOperationException($"A {nameof(IMockDataStore)} must be registered by calling the {nameof(UseStore)} method.");
            }

            Services.AddScoped<IMockingScope, MockingScope>();

            SetupTypeMocker(Services);
        }

        private static void SetupTypeMocker(IServiceCollection services)
        {
            foreach (ServiceDescriptor profiledService in services.Where(s => !s.ServiceType.HasInterface<IServiceWrapper>() && s.IsProfilingEnabled()))
            {
                ServiceDescriptor wrappedService = profiledService.Wrap(services);

                services.Add(wrappedService);
                services.Replace(CreateInterceptor(wrappedService, wrappedService.ServiceType));
            }
        }

        private static ServiceDescriptor CreateInterceptor(ServiceDescriptor service, Type implementationType)
            => new ServiceDescriptor(
                service.ServiceType,
                p => new TypeMockerFactory(p).Create(service.ServiceType, implementationType),
                ServiceLifetime.Transient);
    }
}
