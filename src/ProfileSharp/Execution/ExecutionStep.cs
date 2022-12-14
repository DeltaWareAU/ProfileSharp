using ProfileSharp.Execution.Context;
using System;

namespace ProfileSharp.Execution
{
    public class ExecutionStep : IExecutionStep
    {
        public IExecutionContext ExecutionContext { get; set; } = null!;
        public IExecutedContext ExecutedContext { get; set; } = null!;

        public DateTime UtcTimeStamp { get; } = DateTime.UtcNow;
        public TimeSpan ExecutionTime { get; set; }
    }
}
