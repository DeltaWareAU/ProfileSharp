using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Mocking.Scope;
using ProfileSharp.Wrappers;
using System;

namespace ProfileSharp.Mocking.Factory
{
    internal sealed class TypeMockerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public TypeMockerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object Create(Type definitionType, Type implementationType)
        {
            IServiceWrapper serviceWrapper = (IServiceWrapper)_serviceProvider.GetRequiredService(implementationType);

            TypeMocker mocker = new TypeMocker(_serviceProvider.GetRequiredService<IMockingScope>());

            if (definitionType.IsInterface)
            {
                return _proxyGenerator.CreateInterfaceProxyWithTarget(definitionType, serviceWrapper.Instance, mocker);
            }

            return _proxyGenerator.CreateClassProxyWithTarget(definitionType, serviceWrapper.Instance, mocker);
        }
    }
}
