using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Store;

namespace ProfileSharp.Options
{
    public static class ProfilingOptionsBuilderExtensions
    {
        public static void UseStore<T>(this IProfilingOptionsBuilder builder) where T : class, IProfilingStore
            => builder.Services.AddSingleton<IProfilingStore, T>();

        public static void UseInMemoryStore<T>(this IProfilingOptionsBuilder builder) where T : class, IProfilingStore
            => builder.Services.AddSingleton<IProfilingStore, InMemoryProfilingStore>();
    }
}
