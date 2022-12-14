using System;
using System.Collections.Generic;

namespace ProfileSharp.Execution.Scope
{
    public interface IExecutionScopeContext
    {
        IReadOnlyCollection<IExecutionStep> Steps { get; }

        TimeSpan ExecutionTime { get; }
    }
}
