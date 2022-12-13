using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.AspNetCore.Profiling;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.Profiling.Configuration
{
    public static class ProfilingOptionsBuilderExtensions
    {
        public static void AddControllers(this IProfilingConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Services.AddTransient<ControllerProfiler>();
            configurationBuilder.Services.Configure<MvcOptions>(o => o.Filters.AddService<ControllerProfiler>());
        }
    }
}
