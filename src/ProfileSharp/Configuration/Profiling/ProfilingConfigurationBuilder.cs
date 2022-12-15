using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Scope;
using ProfileSharp.Store;
using System;
using System.Linq;

namespace ProfileSharp.Configuration.Profiling
{
    internal sealed class ProfilingConfigurationBuilder : IProfilingConfigurationBuilder
    {
        public IServiceCollection Services { get; }

        internal ProfilingConfigurationBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public void UseStore<T>() where T : class, IProfilingStore
            => Services.AddScoped<IProfilingStore, T>();

        public void UseStore<T>(T instance) where T : class, IProfilingStore
            => Services.AddSingleton<IProfilingStore, T>(_ => instance);

        public void UseStore<T>(Func<IServiceProvider, T> implementationFactory) where T : class, IProfilingStore
            => Services.AddScoped<IProfilingStore, T>(implementationFactory);

        public void Build()
        {
            if (Services.All(s => s.ServiceType != typeof(IProfilingStore)))
            {
                throw new InvalidOperationException($"A {nameof(IProfilingStore)} must be registered by calling the {nameof(UseStore)} method.");
            }

            Services.TryAddScoped<IProfilingScope, ProfilingScope>();

            foreach (IProfilingConfigurationProvider configuration in ConfigurationProviderHelper.GetConfigurationProviders<IProfilingConfigurationProvider>())
            {
                configuration.Configure(Services);
            }
        }
    }
}
