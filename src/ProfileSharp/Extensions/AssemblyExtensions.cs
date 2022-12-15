﻿using System.Linq;

// ReSharper disable once CheckNamespace
namespace System.Reflection
{
    internal static class AssemblyExtensions
    {
        public static Type[] GetLoadedTypes(this Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException e)
            {
                return e.Types
                    .Where(t => t != null)
                    .ToArray();
            }
        }
    }
}
