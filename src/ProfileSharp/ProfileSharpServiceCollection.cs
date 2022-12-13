using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Configuration;

namespace ProfileSharp
{
    public static class ProfileSharpServiceCollection
    {
        public static IProfileSharpConfiguration AddProfileSharp(this IServiceCollection services)
            => new ProfileSharpConfiguration(services);
    }
}
