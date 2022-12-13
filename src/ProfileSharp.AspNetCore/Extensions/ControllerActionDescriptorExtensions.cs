using ProfileSharp.Attributes;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc.Controllers
{
    internal static class ControllerActionDescriptorExtensions
    {
        public static bool CanProfile(this ControllerActionDescriptor actionDescriptor)
            => DoNotProfile(actionDescriptor) || !Profile(actionDescriptor);

        public static bool DoNotProfile(this ControllerActionDescriptor actionDescriptor)
        {
            if (actionDescriptor.ControllerTypeInfo.HasAttribute<DoNotProfileAttribute>())
            {
                return true;
            }

            return actionDescriptor.MethodInfo.HasAttribute<DoNotProfileAttribute>();
        }

        public static bool Profile(this ControllerActionDescriptor actionDescriptor)
        {
            if (actionDescriptor.ControllerTypeInfo.HasAttribute<ProfileAttribute>())
            {
                return true;
            }

            return actionDescriptor.MethodInfo.HasAttribute<ProfileAttribute>();
        }
    }
}
