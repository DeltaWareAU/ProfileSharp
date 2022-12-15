using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.AspNetCore.Filters;
using ProfileSharp.Configuration.Profiling;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.AspNetCore.Configuration
{
    public class ProfilingConfigurationProvider : IProfilingConfigurationProvider
    {
        public void Configure(IServiceCollection services)
        {
            services.AddScoped<ProfilingFilter>();
            services.Configure<MvcOptions>(o => o.Filters.AddService<ProfilingFilter>());
        }
    }
}
