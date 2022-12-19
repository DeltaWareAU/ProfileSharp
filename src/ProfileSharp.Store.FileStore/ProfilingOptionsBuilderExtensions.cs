using ProfileSharp.Store.FileStore;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.Configuration
{
    public static class ProfilingOptionsBuilderExtensions
    {
        /// <summary>
        /// Use the FileStore to store Profiled Data.
        /// </summary>
        /// <param name="directory">The directory where the profiled data is stored.</param>
        public static void UseFileStore(this IProfilingConfigurationBuilder configurationBuilder, string directory)
            => configurationBuilder.UseStore(new ProfilingFileStore(directory));
    }
}
