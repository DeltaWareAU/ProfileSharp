using Castle.DynamicProxy;
using ProfileSharp.Execution.Context;
using System;

namespace ProfileSharp.Interception
{
    public abstract class TypeInterceptor : IInterceptor
    {
        public Type InterceptedType { get; }

        protected TypeInterceptor(Type interceptedType)
        {
            InterceptedType = interceptedType;
        }

        public void Intercept(IInvocation invocation)
        {
            if (invocation.IsProfileSharpDisable())
            {
                invocation.Proceed();

                return;
            }

            if (invocation.TargetType.AssemblyQualifiedName == null)
            {
                throw new NotSupportedException($"The target type {invocation.TargetType.Name} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            ExecutionContext context = new ExecutionContext
            {
                Arguments = invocation.GetArgumentDictionary(),
                AssemblyQualifiedName = invocation.TargetType.AssemblyQualifiedName,
                MethodName = invocation.Method.Name
            };

            OnIntercept(context, invocation);
        }

        protected abstract void OnIntercept(IExecutionContext context, IInvocation invocation);
    }
}
