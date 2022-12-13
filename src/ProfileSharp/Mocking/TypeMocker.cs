using Castle.DynamicProxy;
using ProfileSharp.Mocking.Scope;
using System;
using System.Collections.Generic;

namespace ProfileSharp.Mocking
{
    internal sealed class TypeMocker : IInterceptor
    {
        private readonly IMockingScope _mockingScope;

        public TypeMocker(IMockingScope mockingScope)
        {
            _mockingScope = mockingScope;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.IsProfilingDisable())
            {
                invocation.Proceed();

                return;
            }

            if (invocation.TargetType.AssemblyQualifiedName == null)
            {
                throw new NotSupportedException($"The target type {invocation.TargetType.Name} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            string assemblyQualifiedName = invocation.TargetType.AssemblyQualifiedName;
            string methodName = invocation.Method.Name;
            IReadOnlyDictionary<string, object> arguments = invocation.GetArgumentDictionary();

            if (_mockingScope.TryGetMockResponseAsync(assemblyQualifiedName, methodName, arguments, out object? mockResponse))
            {
                invocation.ReturnValue = mockResponse;
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
