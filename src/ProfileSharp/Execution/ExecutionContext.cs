using System;
using System.Collections.Generic;

namespace ProfileSharp.Execution
{
    internal sealed class ExecutionContext : IExecutionContext
    {
        public IReadOnlyCollection<IExecutionStep> Steps { get; set; } = null!;

        public TimeSpan ExecutionTime { get; set; }
    }
}
