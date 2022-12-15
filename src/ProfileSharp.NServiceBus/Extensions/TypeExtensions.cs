using ProfileSharp.Attributes;

// ReSharper disable once CheckNamespace
namespace System
{
    internal static class TypeExtensions
    {
        public static bool IsProfileSharpEnabled(this Type type)
            => type.HasAttribute<EnableProfileSharpAttribute>() && !type.HasAttribute<DisableProfileSharpAttribute>();
    }
}
