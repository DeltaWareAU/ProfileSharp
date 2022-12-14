using ProfileSharp.Execution.Context;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Mocking.Scope
{
    public interface IMockingScope
    {
        Task InitiateAsync(IExecutionContext executionContext, CancellationToken cancellationToken = default);

        bool TryGetMockExecutionAsync(IExecutionContext executionContext, out IExecutedContext? executedContext);
    }
}
