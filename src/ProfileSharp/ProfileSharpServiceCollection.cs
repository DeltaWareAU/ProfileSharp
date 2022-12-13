using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Options;
using ProfileSharp.Settings;
using System;

namespace ProfileSharp
{
    public static class ProfileSharpServiceCollection
    {
        public static IServiceCollection AddProfileSharp(this IServiceCollection services, Action<IProfilingOptionsBuilder> optionsAction)
        {
            ProfilingOptionsBuilder optionsBuilder = new ProfilingOptionsBuilder(services);

            optionsAction.Invoke(optionsBuilder);

            optionsBuilder.Build();

            return services;
        }

        public static IServiceCollection DisableProfileSharp(this IServiceCollection services)
        {
            services.Configure<ProfilingSettings>(settings => settings.DisableProfiling = true);

            return services;
        }
    }
}
