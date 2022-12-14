using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfileSharp.Enums;
using ProfileSharp.Execution.Context;
using ProfileSharp.Mocking.Scope;
using ProfileSharp.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileSharp.AspNetCore.Mocking
{
    internal sealed class ControllerMockInitializer : IAsyncActionFilter
    {
        private readonly IMockingScope _profilingScope;
        private readonly ProfileSharpSettings _settings;

        public ControllerMockInitializer(IMockingScope profilingScope, ProfileSharpSettings settings)
        {
            _profilingScope = profilingScope;
            _settings = settings;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_settings.Mode != ProfileSharpMode.Mocking)
            {
                return;
            }

            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            if (string.IsNullOrEmpty(actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName))
            {
                throw new NotSupportedException($"The target controller {actionDescriptor.ControllerTypeInfo.Name} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            if (!actionDescriptor.IsProfileSharpEnabled())
            {
                await next.Invoke();
            }

            var executionContext = new ExecutionContext()
            {
                AssemblyQualifiedName = actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName,
                MethodName = actionDescriptor.MethodInfo.Name,
                Arguments = new Dictionary<string, object>(context.ActionArguments)
            };

            await _profilingScope.InitiateAsync(executionContext);

            await next.Invoke();
        }
    }
}
