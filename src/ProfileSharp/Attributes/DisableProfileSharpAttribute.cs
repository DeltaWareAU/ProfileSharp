using System;

namespace ProfileSharp.Attributes
{
    /// <summary>
    /// Disables ProfileSharp for the specific method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class DisableProfileSharpAttribute : Attribute
    {
    }
}
