using Castle.DynamicProxy;
using ProfileSharp.Execution;
using ProfileSharp.Profiling.Scope;
using System;
using System.Diagnostics;

namespace ProfileSharp.Profiling
{
    internal sealed class TypeProfiler : IInterceptor
    {
        private readonly Stopwatch _invocationStopwatch = new Stopwatch();

        private readonly IProfilingScope _profilingScope;

        public TypeProfiler(IProfilingScope profilingScope)
        {
            _profilingScope = profilingScope;
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

            ExecutionStep executionState = new ExecutionStep
            {
                AssemblyQualifiedName = invocation.TargetType.AssemblyQualifiedName,
                MethodName = invocation.Method.Name,
                Arguments = invocation.GetArgumentDictionary()
            };

            try
            {
                _invocationStopwatch.Start();

                invocation.Proceed();
            }
            catch (Exception e)
            {
                executionState.EncounteredException = e;

                throw;
            }
            finally
            {
                _invocationStopwatch.Stop();

                executionState.ExecutionTime = _invocationStopwatch.Elapsed;
                executionState.ReturnedValue = invocation.ReturnValue;

                _profilingScope.RegisterStep(executionState);
            }
        }
    }
}
