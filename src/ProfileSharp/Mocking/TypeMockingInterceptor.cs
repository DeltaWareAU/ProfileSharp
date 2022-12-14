using Castle.DynamicProxy;
using ProfileSharp.Execution.Context;
using ProfileSharp.Interception;
using ProfileSharp.Mocking.Scope;
using System;

namespace ProfileSharp.Mocking
{
    internal sealed class TypeMockingInterceptor : TypeInterceptor
    {
        private readonly IMockingScope _mockingScope;

        public TypeMockingInterceptor(IMockingScope mockingScope, Type interceptedType) : base(interceptedType)
        {
            _mockingScope = mockingScope;
        }

        protected override void OnIntercept(IExecutionContext context, IInvocation invocation)
        {
            if (_mockingScope.TryGetMockExecutionAsync(context, out IExecutedContext? executedContext))
            {
                if (executedContext!.EncounteredException != null)
                {
                    throw executedContext.EncounteredException;
                }

                invocation.ReturnValue = executedContext.ReturnValue;
            }
            else
            {
                invocation.Proceed();
            }
        }
    }
}
