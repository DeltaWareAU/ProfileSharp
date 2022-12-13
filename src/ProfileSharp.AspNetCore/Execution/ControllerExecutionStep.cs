using System;
using System.Collections.Generic;

namespace ProfileSharp.AspNetCore.Execution
{
    internal sealed class ControllerExecutionStep : IControllerExecutionStep
    {
        public string AssemblyQualifiedName { get; set; } = null!;
        public string MethodName { get; set; } = null!;
        public IReadOnlyDictionary<string, object> Arguments { get; set; } = null!;
        public object? ReturnedValue { get; set; }
        public DateTime TimeStamp { get; } = DateTime.Now;
        public TimeSpan ExecutionTime { get; set; }
        public Exception? EncounteredException { get; set; }

        public string RequestPath { get; set; } = null!;

        public string RequestMethod { get; set; } = null!;
    }
}
