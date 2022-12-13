using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using ProfileSharp.AspNetCore.Execution;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProfileSharp.AspNetCore.Profilers
{
    internal class ControllerProfiler : IActionFilter, IExceptionFilter, IDisposable
    {
        private readonly Stopwatch _invocationStopwatch = new Stopwatch();

        private readonly IProfilingScope _profilingScope;

        private readonly ProfilingSettings _profilingSettings;

        private ControllerExecutionStep? _controllerExecutionStep;

        public ControllerProfiler(IProfilingScope profilingScope, IOptions<ProfilingSettings> options)
        {
            _profilingScope = profilingScope;
            _profilingSettings = options.Value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_profilingSettings.DisableProfiling)
            {
                return;
            }

            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            if (string.IsNullOrEmpty(actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName))
            {
                return;
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
