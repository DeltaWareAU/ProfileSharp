using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static object CreateInstance(this IServiceProvider provider, Type type, params object[] parameters)
            => ActivatorUtilities.CreateInstance(provider, type, parameters);
    }
}
