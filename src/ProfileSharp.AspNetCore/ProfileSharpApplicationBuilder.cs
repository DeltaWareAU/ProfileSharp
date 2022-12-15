using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.AspNetCore;
using ProfileSharp.Settings;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class ProfileSharpApplicationBuilder
    {
        public static IApplicationBuilder UseProfileSharp(this IApplicationBuilder applicationBuilder, Action<IProfileSharpConfiguration> configurationAction)
        {
            configurationAction.Invoke(new ProfileSharpConfiguration(applicationBuilder.ApplicationServices.GetRequiredService<ProfileSharpSettings>()));

            return applicationBuilder;
        }
    }
}
