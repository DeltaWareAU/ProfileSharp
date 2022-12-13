using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static object CreateInstance(this IServiceProvider provider, Type type)
            => ActivatorUtilities.CreateInstance(provider, type);
    }
}
