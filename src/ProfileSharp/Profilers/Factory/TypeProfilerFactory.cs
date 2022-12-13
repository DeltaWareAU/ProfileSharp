using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ProfileSharp.Profilers.Wrappers;
using ProfileSharp.Scope;
using ProfileSharp.Settings;
using System;

namespace ProfileSharp.Profilers.Factory
{
    internal sealed class TypeProfilerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ProxyGenerator _proxyGenerator;

        public TypeProfilerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _proxyGenerator = new ProxyGenerator();
        }

        public object Create(Type definitionType, Type implementationType)
        {
            IServiceWrapper serviceWrapper = (IServiceWrapper)_serviceProvider.GetRequiredService(implementationType);

            TypeProfiler profiler = new TypeProfiler(_serviceProvider.GetRequiredService<IProfilingScope>(), _serviceProvider.GetRequiredService<IOptions<ProfilingSettings>>());

            if (definitionType.IsInterface)
            {
                return _proxyGenerator.CreateInterfaceProxyWithTarget(definitionType, serviceWrapper.Instance, profiler);
            }

            return _proxyGenerator.CreateClassProxyWithTarget(definitionType, serviceWrapper.Instance, profiler);
        }
    }
}
