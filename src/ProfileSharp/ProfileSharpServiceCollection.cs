using ProfileSharp;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProfileSharpServiceCollection
    {
        public static IServiceCollection AddProfileSharp(this IServiceCollection services, Action<IProfileSharpBuilder> builderAction)
        {
            ProfileSharpBuilder builder = new ProfileSharpBuilder(services);

            builder.Build();

            builderAction.Invoke(builder);

            return services;
        }
    }
}
