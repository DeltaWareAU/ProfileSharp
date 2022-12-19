using ProfileSharp.Store.FileStore;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.Configuration
{
    public static class MockingOptionsBuilderExtensions
    {
        /// <summary>
        /// Use the FileStore to retrieve Mocking Data.
        /// </summary>
        /// <param name="directory">The directory where the mock data is stored.</param>
        public static void UseFileStore(this IMockingConfigurationBuilder configurationBuilder, string directory)
            => configurationBuilder.UseStore(new MockDataFileStore(directory));
    }
}
