using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Profiling.Factory;
using ProfileSharp.Profiling.Scope;
using ProfileSharp.Profiling.Store;
using ProfileSharp.Wrappers;
using System;
using System.Linq;

namespace ProfileSharp.Profiling.Configuration
{
    internal sealed class ProfilingConfigurationBuilder : IProfilingConfigurationBuilder
    {
        public IServiceCollection Services { get; }

        internal ProfilingConfigurationBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public void UseStore<T>() where T : class, IProfilingStore
            => Services.AddSingleton<IProfilingStore, T>();

        public void UseStore<T>(Func<IServiceProvider, T> implementationFactory) where T : class, IProfilingStore
            => Services.AddSingleton<IProfilingStore, T>(implementationFactory);

        public void Build()
        {
            if (Services.All(s => s.ServiceType != typeof(IProfilingStore)))
            {
                throw new InvalidOperationException($"A {nameof(IProfilingStore)} must be registered by calling the {nameof(UseStore)} method.");
            }

            Services.TryAddScoped<IProfilingScope, ProfilingScope>();

            SetupTypeProfiler(Services);
        }

        private static void SetupTypeProfiler(IServiceCollection services)
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
                p => new TypeProfilerFactory(p).Create(service.ServiceType, implementationType),
                ServiceLifetime.Transient);
    }
}
