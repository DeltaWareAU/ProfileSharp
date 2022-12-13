using ProfileSharp.Execution;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Mocking.Store
{
    public interface IMockDataStore
    {
        Task<IExecutionContext?> LoadAsync(string assemblyQualifiedName, string methodName, IReadOnlyDictionary<string, object> arguments, CancellationToken cancellationToken = default);
    }
}
