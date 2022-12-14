using ProfileSharp.Execution.Scope;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Profiling.Store
{
    public interface IProfilingStore
    {
        public Task StoreAsync(IExecutionScopeContext scopeContext, CancellationToken cancellationToken = default);
    }
}
