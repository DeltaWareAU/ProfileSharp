using ProfileSharp.Execution;
using ProfileSharp.Profiling.Store;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileSharp.Profiling.Scope
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

            ExecutionContext executionContext = new ExecutionContext
            {
                Steps = _steps.OrderBy(s => s.TimeStamp).ToArray(),
                ExecutionTime = _invocationStopwatch.Elapsed
            };

            await _profilingStore.StoreAsync(executionContext);
        }
    }
}
