using System;
using System.Collections.Generic;

namespace ProfileSharp.Execution
{
    public interface IExecutionStep
    {
        string AssemblyQualifiedName { get; }

        string MethodName { get; }

        IReadOnlyDictionary<string, object> Arguments { get; }

        object? ReturnedValue { get; }

        Exception? EncounteredException { get; }

        DateTime TimeStamp { get; }

        TimeSpan ExecutionTime { get; }
    }
}
