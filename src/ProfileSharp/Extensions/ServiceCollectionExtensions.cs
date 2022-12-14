using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ProfileSharp.Interception.Factory;
using ProfileSharp.Interception.Service;
using System;

namespace ProfileSharp.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInterceptedService(this IServiceCollection services, ServiceDescriptor descriptor)
        {
            ServiceDescriptor interceptedService = GetInterceptedService(descriptor);
            ServiceDescriptor interceptorService = GetInterceptorService(descriptor.ServiceType, interceptedService.ServiceType);

            services.Add(interceptedService);
            services.Replace(interceptorService);

            return services;
        }

        private static ServiceDescriptor GetInterceptorService(Type definitionType, Type implementationType)
            => new ServiceDescriptor(
                definitionType,
                p => p.GetRequiredService<IInterceptorFactory>().Create(definitionType, implementationType),
                ServiceLifetime.Transient);

        private static ServiceDescriptor GetInterceptedService(ServiceDescriptor descriptor)
            => new ServiceDescriptor(
                InterceptedServiceHelper.GetInterceptedServiceType(descriptor.ServiceType),
                p => new InterceptedServiceFactory(descriptor, p).Build(),
                descriptor.Lifetime);
    }
}
