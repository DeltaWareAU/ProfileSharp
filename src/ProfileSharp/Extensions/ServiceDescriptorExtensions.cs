using ProfileSharp.Attributes;
using ProfileSharp.Wrappers;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ServiceDescriptorExtensions
    {
        public static bool IsProfilingEnabled(this ServiceDescriptor descriptor)
        {
            if (descriptor.ServiceType.HasAttribute<EnableProfileSharpAttribute>())
            {
                return true;
            }

            if (descriptor.ServiceType.AnyMethodHasAttribute<EnableProfileSharpAttribute>())
            {
                return true;
            }

            if (descriptor.ImplementationType == null)
            {
                return false;
            }

            if (descriptor.ImplementationType.HasAttribute<EnableProfileSharpAttribute>())
            {
                return true;
            }

            if (descriptor.ImplementationType.AnyMethodHasAttribute<EnableProfileSharpAttribute>())
            {
                return true;
            }

            return false;
        }

        public static ServiceDescriptor Wrap(this ServiceDescriptor descriptor, IServiceCollection services)
        {
            if (descriptor.ImplementationType != null)
            {
                return new ServiceDescriptor(
                    ServiceWrapperHelper.GetWrapperType(descriptor.ImplementationType),
                    p => ServiceWrapperHelper.CreateInstance(descriptor.ImplementationType, p.CreateInstance(descriptor.ImplementationType)),
                    descriptor.Lifetime);
            }

            if (descriptor.ImplementationInstance != null)
            {
                Type implementationType = descriptor.ImplementationInstance.GetType();

                return new ServiceDescriptor(
                    ServiceWrapperHelper.GetWrapperType(implementationType),
                    ServiceWrapperHelper.CreateInstance(implementationType, descriptor.ImplementationInstance));
            }

            if (descriptor.ImplementationFactory != null)
            {
                using ServiceProvider provider = services.BuildServiceProvider();

                Type implementationType = descriptor.ImplementationFactory.Invoke(provider).GetType();

                return new ServiceDescriptor(
                    ServiceWrapperHelper.GetWrapperType(implementationType),
                    p => ServiceWrapperHelper.CreateInstance(implementationType, descriptor.ImplementationFactory.Invoke(p)),
                    descriptor.Lifetime);
            }

            throw new NotSupportedException();
        }
    }
}
