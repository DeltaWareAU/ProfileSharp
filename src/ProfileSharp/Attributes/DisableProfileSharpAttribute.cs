using System;

// ReSharper disable once CheckNamespace
namespace ProfileSharp
{
    /// <summary>
    /// Disables ProfileSharp for the specific method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class DisableProfileSharpAttribute : Attribute
    {
    }
}
