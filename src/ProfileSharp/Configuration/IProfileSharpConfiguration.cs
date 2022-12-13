using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Mocking.Configuration;
using ProfileSharp.Profiling.Configuration;
using System;

namespace ProfileSharp.Configuration
{
    public interface IProfileSharpConfiguration
    {
        IServiceCollection EnableMocking(Action<IMockingConfigurationBuilder> optionsAction);

        IServiceCollection EnableProfiling(Action<IProfilingConfigurationBuilder> optionsAction);
    }
}
