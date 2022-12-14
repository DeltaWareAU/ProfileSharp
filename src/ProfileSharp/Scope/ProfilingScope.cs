using ProfileSharp.Execution;
using ProfileSharp.Execution.Scope;
using ProfileSharp.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileSharp.Scope
{
    internal sealed class ProfilingScope : IProfilingScope, IAsyncDisposable
    {
        private readonly IProfilingStore _profilingStore;

        private readonly List<IExecutionStep> _steps = new List<IExecutionStep>();

        private readonly Stopwatch _invocationStopwatch;

        public ProfilingScope(IProfilingStore profilingStore)
        {
            _profilingStore = profilingStore;
            _invocationStopwatch = Stopwatch.StartNew();
        }

        public void RegisterStep(IExecutionStep step)
        {
            _steps.Add(step);
        }

        public async ValueTask DisposeAsync()
        {
            _invocationStopwatch.Stop();

            if (_steps.Count == 0)
            {
                return;
            }

            ExecutionScopeContext executionScopeContext = new ExecutionScopeContext
            {
                Steps = _steps.OrderBy(s => s.UtcTimeStamp).ToArray(),
                ExecutionTime = _invocationStopwatch.Elapsed
            };

            await _profilingStore.StoreAsync(executionScopeContext);
        }
    }
}
