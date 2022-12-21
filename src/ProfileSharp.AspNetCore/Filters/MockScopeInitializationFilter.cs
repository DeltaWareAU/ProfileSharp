using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfileSharp.AspNetCore.Execution;
using ProfileSharp.Enums;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileSharp.AspNetCore.Filters
{
    internal sealed class MockScopeInitializationFilter : IAsyncActionFilter
    {
        private readonly IMockingScope _profilingScope;
        private readonly IProfileSharpSettings _settings;

        public MockScopeInitializationFilter(IMockingScope profilingScope, IProfileSharpSettings settings)
        {
            _profilingScope = profilingScope;
            _settings = settings;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_settings.Mode != ProfileSharpMode.Mocking)
            {
                await next.Invoke();

                return;
            }

            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            if (!actionDescriptor.IsProfileSharpEnabled())
            {
                await next.Invoke();

                return;
            }

            if (string.IsNullOrEmpty(actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName))
            {
                throw new NotSupportedException($"The target controller {actionDescriptor.ControllerTypeInfo.Name} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            ControllerExecutionContext executionContext = new ControllerExecutionContext
            {
                AssemblyQualifiedName = actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName,
                MethodName = actionDescriptor.MethodInfo.Name,
                Arguments = new Dictionary<string, object>(context.ActionArguments),
                RequestPath = context.HttpContext.Request.Path,
                RequestMethod = context.HttpContext.Request.Method,
            };

            await _profilingScope.InitiateAsync(executionContext);

            await next.Invoke();
        }
    }
}
