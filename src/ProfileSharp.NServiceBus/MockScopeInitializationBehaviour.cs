using NServiceBus.Pipeline;
using ProfileSharp.Enums;
using ProfileSharp.Execution.Context;
using ProfileSharp.NServiceBus.Context;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileSharp.NServiceBus
{
    internal sealed class MockScopeInitializationBehaviour : Behavior<IInvokeHandlerContext>
    {
        private readonly IMockingScope _mockingScope;
        private readonly ProfileSharpSettings _settings;

        public MockScopeInitializationBehaviour(IMockingScope mockingScope, ProfileSharpSettings settings)
        {
            _settings = settings;
            _mockingScope = mockingScope;
        }

        public override async Task Invoke(IInvokeHandlerContext context, Func<Task> next)
        {
            if (_settings.Mode != ProfileSharpMode.Mocking)
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

            ExecutionContext executionContext = new HandleMessageExecutionContext
            {
                AssemblyQualifiedName = context.MessageHandler.HandlerType.AssemblyQualifiedName,
                MethodName = context.MessageBeingHandled.GetType().Name,
                Arguments = new Dictionary<string, object>
                {
                    {"message", context.MessageBeingHandled},
                    {"context", null!}
                }
            };

            await _mockingScope.InitiateAsync(executionContext);
            await next.Invoke();
        }

        internal sealed class Register : RegisterStep
        {
            public Register() : base(nameof(MockScopeInitializationBehaviour), typeof(MockScopeInitializationBehaviour), "Initializes the Mocking Scope for a Message Handle.")
            {
            }
        }
    }
}
