using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Extensions;
using ProfileSharp.Profilers.Factory;
using ProfileSharp.Profilers.Wrappers;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;
using System.Linq;

namespace ProfileSharp.Options
{
    internal sealed class ProfilingOptionsBuilder : IProfilingOptionsBuilder
    {
        public IServiceCollection Services { get; }

        internal ProfilingOptionsBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public void Build()
        {
            Services.TryAddScoped<IProfilingScope, ProfilingScope>();
            Services.Configure<ProfilingSettings>(_ => { });

            SetupTypeProfiler(Services);
        }

        private static void SetupTypeProfiler(IServiceCollection services)
        {
            foreach (ServiceDescriptor profiledService in services.Where(s => s.ServiceType.HasInterface<IServiceWrapper>() && s.IsProfilingEnabled()))
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
