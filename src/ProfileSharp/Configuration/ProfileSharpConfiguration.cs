using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Mocking.Configuration;
using ProfileSharp.Profiling.Configuration;
using System;

namespace ProfileSharp.Configuration
{
    internal sealed class ProfileSharpConfiguration : IProfileSharpConfiguration
    {
        private readonly IServiceCollection _services;

        public ProfileSharpConfiguration(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceCollection EnableMocking(Action<IMockingConfigurationBuilder> optionsAction)
        {
            MockingConfigurationBuilder configurationBuilder = new MockingConfigurationBuilder(_services);

            optionsAction.Invoke(configurationBuilder);

            configurationBuilder.Build();

            return _services;
        }

        public IServiceCollection EnableProfiling(Action<IProfilingConfigurationBuilder> optionsAction)
        {
            ProfilingConfigurationBuilder configurationBuilder = new ProfilingConfigurationBuilder(_services);

            optionsAction.Invoke(configurationBuilder);

            configurationBuilder.Build();

            return _services;
        }
    }
}
