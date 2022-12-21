using System;

// ReSharper disable once CheckNamespace
namespace ProfileSharp
{
    /// <summary>
    /// Enables ProfileSharp for the specified, interface, class or method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method)]
    public sealed class EnableProfileSharpAttribute : Attribute
    {
    }
}