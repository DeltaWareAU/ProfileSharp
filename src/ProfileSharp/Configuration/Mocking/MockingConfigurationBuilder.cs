using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Scope;
using ProfileSharp.Store;
using System;
using System.Linq;

namespace ProfileSharp.Configuration.Mocking
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

        public void UseStore<T>(T instance) where T : class, IMockDataStore
            => Services.AddSingleton<IMockDataStore, T>(_ => instance);

        public void UseStore<T>(Func<IServiceProvider, T> implementationFactory) where T : class, IMockDataStore
            => Services.AddScoped<IMockDataStore, T>(implementationFactory);

        public void Build()
        {
            if (Services.All(s => s.ServiceType != typeof(IMockDataStore)))
            {
                throw new InvalidOperationException($"A {nameof(IMockDataStore)} must be registered by calling the {nameof(UseStore)} method.");
            }

            Services.TryAddScoped<IMockingScope, MockingScope>();

            foreach (IMockingConfigurationProvider configuration in ConfigurationProviderHelper.GetConfigurationProviders<IMockingConfigurationProvider>())
            {
                configuration.Configure(Services);
            }
        }
    }
}
