using ProfileSharp.Configuration.Mocking;
using ProfileSharp.Configuration.Profiling;
using System;

namespace ProfileSharp
{
    public interface IProfileSharpBuilder
    {
        IProfileSharpBuilder AddMocking(Action<IMockingConfigurationBuilder> optionsAction);

        IProfileSharpBuilder AddProfiling(Action<IProfilingConfigurationBuilder> optionsAction);
    }
}
