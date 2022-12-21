using System;

namespace ProfileSharp.Interception.Wrapper
{
    internal static class InterceptedServiceWrapperHelper
    {
        public static Type GetInterceptedServiceType(Type type)
            => typeof(InterceptedServiceWrapper<>).MakeGenericType(type);
    }
}
