using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Profiling.Scope;
using ProfileSharp.Wrappers;
using System;

namespace ProfileSharp.Profiling.Factory
{
    internal sealed class TypeProfilerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public TypeProfilerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object Create(Type definitionType, Type implementationType)
        {
            IServiceWrapper serviceWrapper = (IServiceWrapper)_serviceProvider.GetRequiredService(implementationType);

            TypeProfiler profiler = new TypeProfiler(_serviceProvider.GetRequiredService<IProfilingScope>());

            if (definitionType.IsInterface)
            {
                return _proxyGenerator.CreateInterfaceProxyWithTarget(definitionType, serviceWrapper.Instance, profiler);
            }

            return _proxyGenerator.CreateClassProxyWithTarget(definitionType, serviceWrapper.Instance, profiler);
        }
    }
}
