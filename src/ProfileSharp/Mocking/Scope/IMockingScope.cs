using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Mocking.Scope
{
    public interface IMockingScope
    {
        Task InitiateAsync(string assemblyQualifiedName, string methodName, IReadOnlyDictionary<string, object> arguments, CancellationToken cancellationToken = default);

        bool TryGetMockResponseAsync(string assemblyQualifiedName, string methodName, IReadOnlyDictionary<string, object> arguments, out object? mockResponse);
    }
}
