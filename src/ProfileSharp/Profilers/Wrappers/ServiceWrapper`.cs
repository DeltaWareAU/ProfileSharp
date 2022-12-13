using System;
using System.Threading.Tasks;

namespace ProfileSharp.Profilers.Wrappers
{
    internal sealed class ServiceWrapper<T> : IServiceWrapper, IAsyncDisposable, IDisposable
    {
        public object Instance { get; }

        public ServiceWrapper(T instance)
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
