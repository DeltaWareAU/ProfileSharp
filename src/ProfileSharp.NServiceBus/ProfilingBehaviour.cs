using NServiceBus.Pipeline;
using ProfileSharp.Enums;
using ProfileSharp.Execution;
using ProfileSharp.Execution.Context;
using ProfileSharp.NServiceBus.Context;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ProfileSharp.NServiceBus
{
    internal sealed class ProfilingBehaviour : Behavior<IInvokeHandlerContext>
    {
        private readonly IProfilingScope _profilingScope;
        private readonly ProfileSharpSettings _settings;

        public ProfilingBehaviour(IProfilingScope profilingScope, ProfileSharpSettings settings)
        {
            _settings = settings;
            _profilingScope = profilingScope;
        }

        public override async Task Invoke(IInvokeHandlerContext context, Func<Task> next)
        {
            if (_settings.Mode != ProfileSharpMode.Profiling)
            {
                await next.Invoke();

                return;
            }

            if (!context.MessageHandler.HandlerType.IsProfileSharpEnabled())
            {
                await next.Invoke();

                return;
            }

            if (string.IsNullOrEmpty(context.MessageHandler.HandlerType.AssemblyQualifiedName))
            {
                throw new NotSupportedException($"The target message handler {context.MessageHandler.HandlerType.AssemblyQualifiedName} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            ExecutionStep executionStep = new ExecutionStep
            {
                ExecutionContext = new HandleMessageExecutionContext
                {
                    AssemblyQualifiedName = context.MessageHandler.HandlerType.AssemblyQualifiedName,
                    MethodName = context.MessageBeingHandled.GetType().Name,
                    Arguments = new Dictionary<string, object>
                    {
                        {"message", context.MessageBeingHandled},
                        {"context", null!}
                    }
                }
            };

            ExecutedContext executedContext = new ExecutedContext();

            executionStep.ExecutedContext = executedContext;

            Stopwatch invocationTimer = Stopwatch.StartNew();

            try
            {
                await next.Invoke();
            }
            catch (Exception ex)
            {
                executedContext.EncounteredException = ex;
            }
            finally
            {
                invocationTimer.Stop();

                executionStep.ExecutionTime = invocationTimer.Elapsed;
            }

            _profilingScope.RegisterStep(executionStep);
        }

        internal sealed class Register : RegisterStep
        {
            public Register() : base(nameof(ProfilingBehaviour), typeof(ProfilingBehaviour), "Profiles a Message Handle.")
            {
            }
        }
    }
}
