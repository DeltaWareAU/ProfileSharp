using System;

namespace ProfileSharp.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public sealed class ProfileAttribute : Attribute
    {
    }
}
