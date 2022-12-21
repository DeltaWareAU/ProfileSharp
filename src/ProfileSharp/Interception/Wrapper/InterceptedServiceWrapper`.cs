using System;
using System.Threading.Tasks;

namespace ProfileSharp.Interception.Wrapper
{
    internal sealed class InterceptedServiceWrapper<T> : IInterceptedServiceWrapper, IAsyncDisposable
    {
        public object ServiceInstance { get; }

        public InterceptedServiceWrapper(T serviceInstance)
        {
            ServiceInstance = serviceInstance ?? throw new ArgumentNullException(nameof(serviceInstance));
        }

        public async ValueTask DisposeAsync()
        {
            switch (ServiceInstance)
            {
                case IAsyncDisposable asyncDisposable:
                    await asyncDisposable.DisposeAsync();
                    break;
                case IDisposable disposable:
                    disposable.Dispose();
                    break;
            }
        }
    }
}
