using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Configuration;
using ProfileSharp.Enums;
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

        public void AddMocking(Action<IMockingConfigurationBuilder> optionsAction)
        {
            MockingConfigurationBuilder configurationBuilder = new MockingConfigurationBuilder(_services);

            optionsAction.Invoke(configurationBuilder);

            configurationBuilder.Build();
        }

        public void AddProfiling(Action<IProfilingConfigurationBuilder> optionsAction)
        {
            ProfilingConfigurationBuilder configurationBuilder = new ProfilingConfigurationBuilder(_services);

            optionsAction.Invoke(configurationBuilder);

            configurationBuilder.Build();
        }

        public void SetMode(ProfileSharpMode mode)
        {
            _services.AddSingleton<IProfileSharpSettings>(new ProfileSharpSettings
            {
                Mode = mode
            });
        }

        public void Build()
        {
            _services.TryAddScoped<IInterceptorFactory, TypeInterceptorFactory>();
            _services.TryAddSingleton<IProfileSharpSettings, ProfileSharpSettings>();

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
