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
            if (actionDescriptor.ControllerTypeInfo.HasAttribute<DisableProfileSharpAttribute>())
            {
                return true;
            }

            return actionDescriptor.MethodInfo.HasAttribute<DisableProfileSharpAttribute>();
        }

        public static bool Profile(this ControllerActionDescriptor actionDescriptor)
        {
            if (actionDescriptor.ControllerTypeInfo.HasAttribute<EnableProfileSharpAttribute>())
            {
                return true;
            }

            return actionDescriptor.MethodInfo.HasAttribute<EnableProfileSharpAttribute>();
        }
    }
}
