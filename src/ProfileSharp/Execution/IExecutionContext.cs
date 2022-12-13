using System;
using System.Collections.Generic;

namespace ProfileSharp.Execution
{
    public interface IExecutionContext
    {
        IReadOnlyCollection<IExecutionStep> Steps { get; }

        TimeSpan ExecutionTime { get; }
    }
}
