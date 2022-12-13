using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfileSharp.AspNetCore.Execution;
using ProfileSharp.Profiling.Scope;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProfileSharp.AspNetCore.Profiling
{
    internal class ControllerProfiler : IActionFilter, IExceptionFilter, IDisposable
    {
        private readonly Stopwatch _invocationStopwatch = new Stopwatch();

        private readonly IProfilingScope _profilingScope;

        private ControllerExecutionStep? _controllerExecutionStep;

        public ControllerProfiler(IProfilingScope profilingScope)
        {
            _profilingScope = profilingScope;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            if (string.IsNullOrEmpty(actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName))
            {
                throw new NotSupportedException($"The target controller {actionDescriptor.ControllerTypeInfo.Name} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            if (!actionDescriptor.CanProfile())
            {
                return;
            }

            _controllerExecutionStep = new ControllerExecutionStep
            {
                AssemblyQualifiedName = actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName,
                MethodName = actionDescriptor.MethodInfo.Name,
                Arguments = new Dictionary<string, object>(context.ActionArguments),
                RequestPath = context.HttpContext.Request.Path,
                RequestMethod = context.HttpContext.Request.Method,
            };

            _invocationStopwatch.Start();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_controllerExecutionStep == null)
            {
                return;
            }

            if (context.Result is ObjectResult objectResult)
            {
                _controllerExecutionStep.ReturnedValue = objectResult.Value;
            }
            else
            {
                _controllerExecutionStep.ReturnedValue = context.Result;
            }
        }

        public void OnException(ExceptionContext context)
        {
            if (_controllerExecutionStep == null)
            {
                return;
            }

            _controllerExecutionStep.EncounteredException = context.Exception;
        }

        public void Dispose()
        {
            if (_controllerExecutionStep == null)
            {
                return;
            }

            _profilingScope.RegisterStep(_controllerExecutionStep);
        }
    }
}
