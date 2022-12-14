using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Mocking.Store;
using System;

namespace ProfileSharp.Mocking.Configuration
{
    public interface IMockingConfigurationBuilder
    {
        IServiceCollection Services { get; }

        void UseStore<T>() where T : class, IMockDataStore;
        void UseStore<T>(T instance) where T : class, IMockDataStore;
        void UseStore<T>(Func<IServiceProvider, T> implementationFactory) where T : class, IMockDataStore;
    }
}
