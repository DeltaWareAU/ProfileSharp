using System;
using System.Collections.Generic;

namespace ProfileSharp.Execution.Scope
{
    public class ExecutionScopeContext : IExecutionScopeContext
    {
        public IReadOnlyCollection<IExecutionStep> Steps { get; set; } = null!;

        public TimeSpan ExecutionTime { get; set; }
    }
}
