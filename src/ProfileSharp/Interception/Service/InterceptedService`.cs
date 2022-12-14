using System;
using System.Threading.Tasks;

namespace ProfileSharp.Interception.Service
{
    internal sealed class InterceptedService<T> : IInterceptedService, IAsyncDisposable, IDisposable
    {
        public object Instance { get; }

        public InterceptedService(T instance)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        public void Dispose()
        {
            if (Instance is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (Instance is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
        }
    }
}
