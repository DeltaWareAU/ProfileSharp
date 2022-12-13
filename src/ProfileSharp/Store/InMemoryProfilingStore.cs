using ProfileSharp.Execution;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProfileSharp.Store
{
    public class InMemoryProfilingStore : IProfilingStore
    {
        private readonly List<IExecutionContext> _executions = new List<IExecutionContext>();

        public IReadOnlyCollection<IExecutionContext> Executions => _executions;

        public Task StoreAsync(IExecutionContext context, CancellationToken cancellationToken = default)
        {
            _executions.Add(context);

            return Task.CompletedTask;
        }
    }
}
