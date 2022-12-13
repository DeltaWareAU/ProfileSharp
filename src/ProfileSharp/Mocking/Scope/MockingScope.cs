using ProfileSharp.Execution;
using ProfileSharp.Mocking.Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Mocking.Scope
{
    internal sealed class MockingScope : IMockingScope
    {
        private readonly IMockDataStore _mockDataStore;

        private IExecutionContext? _mockExecutionContext;

        public MockingScope(IMockDataStore mockDataStore)
        {
            _mockDataStore = mockDataStore;
        }

        public async Task InitiateAsync(string assemblyQualifiedName, string methodName, IReadOnlyDictionary<string, object> arguments, CancellationToken cancellationToken = default)
        {
            _mockExecutionContext = await _mockDataStore.LoadAsync(assemblyQualifiedName, methodName, arguments, cancellationToken);
        }

        public bool TryGetMockResponseAsync(string assemblyQualifiedName, string methodName, IReadOnlyDictionary<string, object> arguments, out object? mockResponse)
        {
            mockResponse = null;

            // TODO: Arguments need to be matched.
            IExecutionStep? mockExecution = _mockExecutionContext?.Steps
                .SingleOrDefault(s => s.AssemblyQualifiedName == assemblyQualifiedName && s.MethodName == methodName);

            if (mockExecution == null)
            {
                return false;
            }

            mockResponse = mockExecution.ReturnedValue;

            return true;
        }
    }
}
