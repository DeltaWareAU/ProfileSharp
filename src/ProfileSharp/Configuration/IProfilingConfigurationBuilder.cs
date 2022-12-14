using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Store;
using System;

namespace ProfileSharp.Configuration
{
    public interface IProfilingConfigurationBuilder
    {
        IServiceCollection Services { get; }

        void UseStore<T>() where T : class, IProfilingStore;
        void UseStore<T>(T instance) where T : class, IProfilingStore;
        void UseStore<T>(Func<IServiceProvider, T> implementationFactory) where T : class, IProfilingStore;
    }
}
