using System;

namespace ProfileSharp.Profilers.Wrappers
{
    internal static class ServiceWrapperHelper
    {
        public static Type GetWrapperType(Type type)
            => typeof(ServiceWrapper<>).MakeGenericType(type);

        public static object CreateInstance(Type type, params object?[]? args)
            => Activator.CreateInstance(GetWrapperType(type), args);
    }
}
