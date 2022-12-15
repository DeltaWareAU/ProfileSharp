using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Enums;
using ProfileSharp.Interception.Service;
using ProfileSharp.Settings;
using System;

namespace ProfileSharp.Interception.Factory
{
    internal class TypeInterceptorFactory : IInterceptorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ProfileSharpSettings _settings;

        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public TypeInterceptorFactory(IServiceProvider serviceProvider, ProfileSharpSettings settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings;
        }

        public object Create(Type definitionType, Type implementationType)
        {
            IInterceptedService serviceWrapper = (IInterceptedService)_serviceProvider.GetRequiredService(implementationType);

            return _settings.Mode switch
            {
                ProfileSharpMode.Disabled => serviceWrapper.ServiceInstance,
                ProfileSharpMode.Profiling => CreateInterceptor<TypeProfilingInterceptor>(serviceWrapper, definitionType),
                ProfileSharpMode.Mocking => CreateInterceptor<TypeMockingInterceptor>(serviceWrapper, definitionType),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private object CreateInterceptor<TInterceptor>(IInterceptedService serviceWrapper, Type definitionType) where TInterceptor : TypeInterceptor
        {
            TypeInterceptor interceptor = (TypeInterceptor)_serviceProvider.CreateInstance(typeof(TInterceptor), definitionType);

            if (definitionType.IsInterface)
            {
                return _proxyGenerator.CreateInterfaceProxyWithTarget(interceptor.InterceptedType, serviceWrapper.ServiceInstance, interceptor);
            }

            return _proxyGenerator.CreateClassProxyWithTarget(interceptor.InterceptedType, serviceWrapper.ServiceInstance, interceptor);
        }
    }
}
