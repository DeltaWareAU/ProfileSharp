using ProfileSharp.Attributes;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    internal static class ServiceDescriptorExtensions
    {
        public static bool IsProfileSharpEnabled(this ServiceDescriptor descriptor)
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
    }
}
