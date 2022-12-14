using ProfileSharp.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Castle.DynamicProxy
{
    internal static class InvocationExtensions
    {
        public static IReadOnlyDictionary<string, object> GetArgumentDictionary(this IInvocation invocation)
        {
            object[] argumentValues = invocation.Arguments;
            string[] argumentNames = invocation.Method.GetParameters().Select(p => p.Name).ToArray();

            Dictionary<string, object> arguments = new Dictionary<string, object>();

            for (int i = 0; i < argumentNames.Length; i++)
            {
                arguments.Add(argumentNames[i], argumentValues[i]);
            }

            return arguments;
        }

        public static bool IsProfileSharpDisable(this IInvocation invocation)
            => invocation.TargetType.HasAttribute<DisableProfileSharpAttribute>() ||
               invocation.MethodInvocationTarget.HasAttribute<DisableProfileSharpAttribute>();
    }
}
