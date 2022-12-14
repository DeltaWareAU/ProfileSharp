using ProfileSharp.Execution.Context;
using System;

namespace ProfileSharp.Execution
{
    public interface IExecutionStep
    {
        IExecutionContext ExecutionContext { get; }

        IExecutedContext ExecutedContext { get; }

        DateTime UtcTimeStamp { get; }

        TimeSpan ExecutionTime { get; }
    }
}
