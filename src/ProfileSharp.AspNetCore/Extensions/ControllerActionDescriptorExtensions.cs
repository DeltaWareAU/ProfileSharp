using ProfileSharp;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Mvc.Controllers
{
    internal static class ControllerActionDescriptorExtensions
    {
        public static bool IsProfileSharpEnabled(this ControllerActionDescriptor actionDescriptor)
            => !IsDisabled(actionDescriptor) && IsEnabled(actionDescriptor);

        private static bool IsDisabled(ControllerActionDescriptor actionDescriptor)
            => actionDescriptor.MethodInfo.HasAttribute<DisableProfileSharpAttribute>();

        private static bool IsEnabled(ControllerActionDescriptor actionDescriptor)
            => actionDescriptor.ControllerTypeInfo.HasAttribute<EnableProfileSharpAttribute>() ||
               actionDescriptor.MethodInfo.HasAttribute<EnableProfileSharpAttribute>();
    }
}
