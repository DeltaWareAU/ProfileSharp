using ProfileSharp;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProfileSharpServiceCollection
    {
        /// <summary>
        /// Adds the required dependencies for ProfileSharp.
        /// </summary>
        /// <param name="builderAction">The options used to configure ProfileSharp.</param>
        /// <remarks>This method does not enable ProfileSharp, please call the UseProfileSharp method.</remarks>
        public static IServiceCollection AddProfileSharp(this IServiceCollection services, Action<IProfileSharpBuilder> builderAction)
        {
            ProfileSharpBuilder builder = new ProfileSharpBuilder(services);

            builder.Build();

            builderAction.Invoke(builder);

            return services;
        }
    }
}
