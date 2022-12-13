using Castle.Core.Internal;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProfileSharp.AspNetCore")]
// ReSharper disable once CheckNamespace
namespace System
{
    internal static class MethodInfoExtensions
    {
        public static bool HasAttribute<TAttribute>(this MethodInfo method) where TAttribute : Attribute
            => method.GetAttribute<TAttribute>() != null;
    }
}
