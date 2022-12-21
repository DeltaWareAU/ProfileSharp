using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Interception.Wrapper;
using System;

namespace ProfileSharp.Interception.Factory
{
    internal sealed class InterceptedServiceWrapperFactory
    {
        private readonly ServiceDescriptor _descriptor;
        private readonly IServiceProvider _provider;

        public InterceptedServiceWrapperFactory(ServiceDescriptor descriptor, IServiceProvider provider)
        {
            _descriptor = descriptor;
            _provider = provider;
        }

        public IInterceptedServiceWrapper Build()
        {
            object serviceInstance;

            if (_descriptor.ImplementationType != null)
            {
                serviceInstance = _provider.CreateInstance(_descriptor.ImplementationType);
            }
            else if (_descriptor.ImplementationInstance != null)
            {
                serviceInstance = _descriptor.ImplementationInstance;
            }
            else if (_descriptor.ImplementationFactory != null)
            {
                serviceInstance = _descriptor.ImplementationFactory.Invoke(_provider);
            }
            else
            {
                throw new NotSupportedException();
            }

            Type interceptedServiceType = InterceptedServiceWrapperHelper.GetInterceptedServiceType(_descriptor.ServiceType);

            return (IInterceptedServiceWrapper)Activator.CreateInstance(interceptedServiceType, serviceInstance);
        }
    }
}
