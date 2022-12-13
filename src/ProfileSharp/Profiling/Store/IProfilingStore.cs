using ProfileSharp.Execution;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Profiling.Store
{
    public interface IProfilingStore
    {
        public Task StoreAsync(IExecutionContext context, CancellationToken cancellationToken = default);
    }
}
