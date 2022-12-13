using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Profiling.Store;
using System;

namespace ProfileSharp.Profiling.Configuration
{
    public interface IProfilingConfigurationBuilder
    {
        IServiceCollection Services { get; }

        void UseStore<T>() where T : class, IProfilingStore;

        void UseStore<T>(Func<IServiceProvider, T> implementationFactory) where T : class, IProfilingStore;
    }
}
