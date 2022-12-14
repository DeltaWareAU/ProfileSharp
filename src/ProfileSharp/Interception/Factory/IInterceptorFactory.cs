using System;

namespace ProfileSharp.Interception.Factory
{
    internal interface IInterceptorFactory
    {
        object Create(Type definitionType, Type implementationType);
    }
}
