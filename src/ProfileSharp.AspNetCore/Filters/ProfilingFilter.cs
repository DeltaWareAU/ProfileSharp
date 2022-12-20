using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfileSharp.AspNetCore.Execution;
using ProfileSharp.Enums;
using ProfileSharp.Execution;
using ProfileSharp.Execution.Context;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProfileSharp.AspNetCore.Filters
{
    internal sealed class ProfilingFilter : IActionFilter, IExceptionFilter, IDisposable
    {
        private readonly Stopwatch _invocationStopwatch = new Stopwatch();

        private readonly IProfilingScope _profilingScope;
        private readonly ProfileSharpSettings _settings;

        private ExecutionStep? _controllerExecutionStep;

        private ExecutedContext? _executedContext;

        public ProfilingFilter(IProfilingScope profilingScope, ProfileSharpSettings settings)
        {
            _profilingScope = profilingScope;
            _settings = settings;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_settings.Mode != ProfileSharpMode.Profiling)
            {
                return;
            }

            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            if (!actionDescriptor.IsProfileSharpEnabled())
            {
                return;
            }

            if (string.IsNullOrEmpty(actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName))
            {
                throw new NotSupportedException($"The target controller {actionDescriptor.ControllerTypeInfo.Name} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            _executedContext = new ExecutedContext();

            _controllerExecutionStep = new ExecutionStep
            {
                ExecutionContext = new ControllerExecutionContext
                {
                    AssemblyQualifiedName = actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName,
                    MethodName = actionDescriptor.MethodInfo.Name,
                    Arguments = new Dictionary<string, object>(context.ActionArguments),
                    RequestPath = context.HttpContext.Request.Path,
                    RequestMethod = context.HttpContext.Request.Method,
                },
                ExecutedContext = _executedContext
            };

            _invocationStopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_executedContext == null)
            {
                return;
            }

            if (context.Result is ObjectResult objectResult)
            {
                _executedContext.ReturnValue = objectResult.Value;
            }
            else
            {
                _executedContext.ReturnValue = context.Result;
            }
        }

        public void OnException(ExceptionContext context)
        {
            if (_executedContext == null)
            {
                return;
            }

            _executedContext.EncounteredException = context.Exception;
        }

        public void Dispose()
        {
            if (_controllerExecutionStep == null)
            {
                return;
            }

            _invocationStopwatch.Stop();

            _controllerExecutionStep.ExecutionTime = _invocationStopwatch.Elapsed;

            _profilingScope.RegisterStep(_controllerExecutionStep);
        }
    }
}
