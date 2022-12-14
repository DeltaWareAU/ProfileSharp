using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.AspNetCore;
using ProfileSharp.Settings;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class ProfileSharpApplicationBuilder
    {
        public static IProfileSharpConfiguration UseProfileSharp(this IApplicationBuilder applicationBuilder)
            => new ProfileSharpConfiguration(applicationBuilder.ApplicationServices.GetRequiredService<ProfileSharpSettings>());
    }
}
