using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Configuration;
using ProfileSharp.Extensions;
using ProfileSharp.Interception.Factory;
using ProfileSharp.Settings;
using System;
using System.Linq;

namespace ProfileSharp
{
    internal sealed class ProfileSharpBuilder : IProfileSharpBuilder
    {
        private readonly IServiceCollection _services;

        public ProfileSharpBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public IProfileSharpBuilder AddMocking(Action<IMockingConfigurationBuilder> optionsAction)
        {
            MockingConfigurationBuilder configurationBuilder = new MockingConfigurationBuilder(_services);

            optionsAction.Invoke(configurationBuilder);

            configurationBuilder.Build();

            return this;
        }

        public IProfileSharpBuilder AddProfiling(Action<IProfilingConfigurationBuilder> optionsAction)
        {
            ProfilingConfigurationBuilder configurationBuilder = new ProfilingConfigurationBuilder(_services);

            optionsAction.Invoke(configurationBuilder);

            configurationBuilder.Build();

            return this;
        }

        public void Build()
        {
            _services.TryAddScoped<IInterceptorFactory, TypeInterceptorFactory>();
            _services.TryAddSingleton<ProfileSharpSettings>();

            CreateServiceInterceptors(_services);
        }

        private static void CreateServiceInterceptors(IServiceCollection services)
        {
            var interceptServices = services.Where(s => s.IsProfileSharpEnabled()).ToArray();

            foreach (ServiceDescriptor interceptService in interceptServices)
            {
                services.AddInterceptedService(interceptService);
            }
        }
    }
}
