using ProfileSharp.Execution;
using ProfileSharp.Execution.Context;
using ProfileSharp.Execution.Scope;
using ProfileSharp.Store;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Scope
{
    internal sealed class MockingScope : IMockingScope
    {
        private readonly IMockDataStore _mockDataStore;

        private IExecutionScopeContext? _mockExecutionContext;

        public MockingScope(IMockDataStore mockDataStore)
        {
            _mockDataStore = mockDataStore;
        }

        public async Task InitiateAsync(IExecutionContext executionContext, CancellationToken cancellationToken = default)
        {
            _mockExecutionContext = await _mockDataStore.GetExecutionScopeAsync(executionContext, cancellationToken);
        }

        public bool TryGetMockExecutionAsync(IExecutionContext executionContext, out IExecutedContext? executedContext)
        {
            string contextHash = executionContext.ComputeHash();

            // TODO: Arguments need to be matched.
            IExecutionStep? mockExecution = _mockExecutionContext?.Steps
                .FirstOrDefault(s => s.ExecutionContext.ComputeHash() == contextHash);

            if (mockExecution == null)
            {
                executedContext = null;

                return false;
            }

            executedContext = mockExecution.ExecutedContext;

            return true;
        }
    }
}
