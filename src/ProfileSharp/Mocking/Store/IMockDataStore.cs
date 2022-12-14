using ProfileSharp.Execution.Context;
using ProfileSharp.Execution.Scope;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Mocking.Store
{
    public interface IMockDataStore
    {
        Task<IExecutionScopeContext?> GetExecutionScopeAsync(IExecutionContext executionContext, CancellationToken cancellationToken = default);
    }
}
