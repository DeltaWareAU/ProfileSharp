using Castle.DynamicProxy;
using Microsoft.Extensions.Options;
using ProfileSharp.Execution;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;
using System.Diagnostics;

namespace ProfileSharp.Profilers
{
    internal sealed class TypeProfiler : IInterceptor
    {
        private readonly Stopwatch _invocationStopwatch = new Stopwatch();

        private readonly IProfilingScope _profilingScope;

        private readonly ProfilingSettings _profilingSettings;

        public TypeProfiler(IProfilingScope profilingScope, IOptions<ProfilingSettings> options)
        {
            _profilingScope = profilingScope;
            _profilingSettings = options.Value;
        }

        public void Intercept(IInvocation invocation)
        {
            if (_profilingSettings.DisableProfiling)
            {
                invocation.Proceed();

                return;
            }

            if (string.IsNullOrEmpty(invocation.TargetType.AssemblyQualifiedName))
            {
                invocation.Proceed();

                return;
            }

            if (invocation.IsProfilingDisable())
            {
                invocation.Proceed();

                return;
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
