using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Enums;
using ProfileSharp.Interception.Wrapper;
using ProfileSharp.Settings;
using System;

namespace ProfileSharp.Interception.Factory
{
    internal sealed class TypeInterceptorFactory : IInterceptorFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IProfileSharpSettings _settings;

        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public TypeInterceptorFactory(IServiceProvider serviceProvider, IProfileSharpSettings settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings;
        }

        public object Create(Type definitionType, Type implementationType)
        {
            IInterceptedServiceWrapper serviceWrapper = (IInterceptedServiceWrapper)_serviceProvider.GetRequiredService(implementationType);

            return _settings.Mode switch
            {
                ProfileSharpMode.Disabled => serviceWrapper.ServiceInstance,
                ProfileSharpMode.Profiling => CreateInterceptor<TypeProfilingInterceptor>(serviceWrapper, definitionType),
                ProfileSharpMode.Mocking => CreateInterceptor<TypeMockingInterceptor>(serviceWrapper, definitionType),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private object CreateInterceptor<TInterceptor>(IInterceptedServiceWrapper serviceWrapper, Type definitionType) where TInterceptor : TypeInterceptor
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
