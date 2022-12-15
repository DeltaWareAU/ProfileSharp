using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ProfileSharp.Configuration
{
    internal static class ConfigurationProviderHelper
    {
        public static IEnumerable<T> GetConfigurationProviders<T>() where T : IConfigurationProvider
            => AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetLoadedTypes())
                .Where(t => !t.IsAbstract && t.HasInterface<T>())
                .Select(t => (T)Activator.CreateInstance(t));
    }
}
