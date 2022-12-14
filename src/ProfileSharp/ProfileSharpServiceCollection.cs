using ProfileSharp;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ProfileSharpServiceCollection
    {
        public static IProfileSharpBuilder AddProfileSharp(this IServiceCollection services)
        {
            ProfileSharpBuilder builder = new ProfileSharpBuilder(services);

            builder.Build();

            return builder;
        }
    }
}
