using System;
using System.Threading.Tasks;

namespace ProfileSharp.Interception.Service
{
    internal sealed class InterceptedService<T> : IInterceptedService, IAsyncDisposable
    {
        public object ServiceInstance { get; }

        public InterceptedService(T serviceInstance)
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
