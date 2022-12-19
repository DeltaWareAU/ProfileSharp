using ProfileSharp.Configuration;
using System;

namespace ProfileSharp
{
    public interface IProfileSharpBuilder
    {
        /// <summary>
        /// Adds the required dependencies for Mocking.
        /// </summary>
        /// <param name="optionsAction">The options used to configure Mocking.</param>
        void AddMocking(Action<IMockingConfigurationBuilder> optionsAction);

        /// <summary>
        /// Adds the required dependencies for Profiling.
        /// </summary>
        /// <param name="optionsAction">The options used to configure Profiling.</param>
        void AddProfiling(Action<IProfilingConfigurationBuilder> optionsAction);
    }
}
