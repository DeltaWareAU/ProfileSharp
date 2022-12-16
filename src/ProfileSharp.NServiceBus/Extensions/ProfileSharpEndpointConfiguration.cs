using ProfileSharp.NServiceBus;

// ReSharper disable once CheckNamespace
namespace NServiceBus
{
    public static class ProfileSharpEndpointConfiguration
    {
        public static void UseProfileSharp(this EndpointConfiguration configuration)
        {
            configuration.RegisterComponents(components =>
            {
                components.ConfigureComponent<ProfilingBehaviour>(DependencyLifecycle.SingleInstance);
                components.ConfigureComponent<MockScopeInitializationBehaviour>(DependencyLifecycle.SingleInstance);
            });

            configuration.Pipeline.Register<ProfilingBehaviour.Register>();
            configuration.Pipeline.Register<MockScopeInitializationBehaviour.Register>();
        }
    }
}
