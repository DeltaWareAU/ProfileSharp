using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.Interception.Service;
using System;

namespace ProfileSharp.Interception.Factory
{
    internal sealed class InterceptedServiceFactory
    {
        private readonly ServiceDescriptor _descriptor;
        private readonly IServiceProvider _provider;

        public InterceptedServiceFactory(ServiceDescriptor descriptor, IServiceProvider provider)
        {
            _descriptor = descriptor;
            _provider = provider;
        }

        public IInterceptedService Build()
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

            Type interceptedServiceType = InterceptedServiceHelper.GetInterceptedServiceType(_descriptor.ServiceType);

            return (IInterceptedService)Activator.CreateInstance(interceptedServiceType, serviceInstance);
        }
    }
}
