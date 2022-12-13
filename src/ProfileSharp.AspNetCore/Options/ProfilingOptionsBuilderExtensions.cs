using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.AspNetCore.Profilers;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.Options
{
    public static class ProfilingOptionsBuilderExtensions
    {
        public static void ProfileControllers(this IProfilingOptionsBuilder optionsBuilder)
        {
            optionsBuilder.Services.AddTransient<ControllerProfiler>();
            optionsBuilder.Services.Configure<MvcOptions>(o => o.Filters.AddService<ControllerProfiler>());
        }
    }
}
