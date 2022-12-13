using Castle.Core.Internal;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ProfileSharp.AspNetCore")]
// ReSharper disable once CheckNamespace
namespace System
{
    internal static class TypeExtensions
    {
        public static bool HasInterface<TInterface>(this Type type)
            => typeof(TInterface).IsAssignableFrom(type);

        public static bool HasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
            => type.GetAttribute<TAttribute>() != null;

        public static bool AnyMethodHasAttribute<TAttribute>(this Type type) where TAttribute : Attribute
            => type
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Any(m => m.HasAttribute<TAttribute>());
    }
}
