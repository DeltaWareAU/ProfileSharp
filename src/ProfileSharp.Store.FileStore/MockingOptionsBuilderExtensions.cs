using ProfileSharp.Configuration;
using ProfileSharp.Store.FileStore;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.Profiling.Configuration
{
    public static class MockingOptionsBuilderExtensions
    {
        public static void UseFileStore(this IMockingConfigurationBuilder configurationBuilder, string directory)
            => configurationBuilder.UseStore(new MockDataFileStore(directory));
    }
}
