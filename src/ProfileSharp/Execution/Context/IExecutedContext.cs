using System;

namespace ProfileSharp.Execution.Context
{
    public interface IExecutedContext
    {
        object? ReturnValue { get; }

        Exception? EncounteredException { get; }
    }
}
