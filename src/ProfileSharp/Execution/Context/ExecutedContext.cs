using System;

namespace ProfileSharp.Execution.Context
{
    public class ExecutedContext : IExecutedContext
    {
        public object? ReturnValue { get; set; }

        public Exception? EncounteredException { get; set; }
    }
}
