using Castle.DynamicProxy;
using ProfileSharp.Execution;
using ProfileSharp.Execution.Context;
using ProfileSharp.Interception;
using ProfileSharp.Profiling.Scope;
using System;
using System.Diagnostics;

namespace ProfileSharp.Profiling
{
    internal sealed class TypeProfilingInterceptor : TypeInterceptor
    {
        private readonly Stopwatch _invocationStopwatch = new Stopwatch();

        private readonly IProfilingScope _profilingScope;

        public TypeProfilingInterceptor(IProfilingScope profilingScope, Type interceptedType) : base(interceptedType)
        {
            _profilingScope = profilingScope;
        }

        protected override void OnIntercept(IExecutionContext context, IInvocation invocation)
        {
            ExecutedContext executedContext = new ExecutedContext();

            ExecutionStep executionState = new ExecutionStep
            {
                ExecutionContext = context,
                ExecutedContext = executedContext
            };

            try
            {
                _invocationStopwatch.Start();

                invocation.Proceed();

                executedContext.ReturnValue = invocation.ReturnValue;
            }
            catch (Exception e)
            {
                executedContext.EncounteredException = e;

                throw;
            }
            finally
            {
                _invocationStopwatch.Stop();

                executionState.ExecutionTime = _invocationStopwatch.Elapsed;

                _profilingScope.RegisterStep(executionState);
            }
        }
    }
}
