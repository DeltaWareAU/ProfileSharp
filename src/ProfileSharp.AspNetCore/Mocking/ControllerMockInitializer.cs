using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using ProfileSharp.Mocking.Scope;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileSharp.AspNetCore.Mocking
{
    internal sealed class ControllerMockInitializer : IAsyncActionFilter
    {
        private readonly IMockingScope _profilingScope;

        public ControllerMockInitializer(IMockingScope profilingScope)
        {
            _profilingScope = profilingScope;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerActionDescriptor actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;

            if (string.IsNullOrEmpty(actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName))
            {
                throw new NotSupportedException($"The target controller {actionDescriptor.ControllerTypeInfo.Name} is not supported by profile sharp as it does not have a {nameof(Type.AssemblyQualifiedName)}.");
            }

            if (!actionDescriptor.CanProfile())
            {
                await next.Invoke();
            }

            string assemblyQualifiedName = actionDescriptor.ControllerTypeInfo.AssemblyQualifiedName;
            string methodName = actionDescriptor.MethodInfo.Name;
            IReadOnlyDictionary<string, object> arguments = new Dictionary<string, object>(context.ActionArguments);

            await _profilingScope.InitiateAsync(assemblyQualifiedName, methodName, arguments);

            await next.Invoke();
        }
    }
}
