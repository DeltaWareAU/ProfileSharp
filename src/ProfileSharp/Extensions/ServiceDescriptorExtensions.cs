using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Attributes;
using ProfileSharp.Profilers.Wrappers;
using System;

namespace ProfileSharp.Extensions
{
    internal static class ServiceDescriptorExtensions
    {
        public static bool IsProfilingEnabled(this ServiceDescriptor descriptor)
        {
            if (descriptor.ServiceType.HasAttribute<ProfileAttribute>())
            {
                return true;
            }

            if (descriptor.ServiceType.AnyMethodHasAttribute<ProfileAttribute>())
            {
                return true;
            }

            if (descriptor.ImplementationType == null)
            {
                return false;
            }

            if (descriptor.ImplementationType.HasAttribute<ProfileAttribute>())
            {
                return true;
            }

            if (descriptor.ImplementationType.AnyMethodHasAttribute<ProfileAttribute>())
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
