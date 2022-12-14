using System;

namespace ProfileSharp.Interception.Service
{
    internal static class InterceptedServiceHelper
    {
        public static Type GetInterceptedServiceType(Type type)
            => typeof(InterceptedService<>).MakeGenericType(type);
    }
}
