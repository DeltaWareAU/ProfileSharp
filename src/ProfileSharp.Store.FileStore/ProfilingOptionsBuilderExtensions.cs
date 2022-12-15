using ProfileSharp.Configuration;
using ProfileSharp.Store.FileStore;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.Profiling.Configuration
{
    public static class ProfilingOptionsBuilderExtensions
    {
        public static void UseFileStore(this IProfilingConfigurationBuilder configurationBuilder, string directory)
            => configurationBuilder.UseStore(new ProfilingFileStore(directory));
    }
}
