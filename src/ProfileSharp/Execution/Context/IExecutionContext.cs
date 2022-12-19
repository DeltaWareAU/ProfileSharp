using System.Collections.Generic;

namespace ProfileSharp.Execution.Context
{
    public interface IExecutionContext
    {
        string AssemblyQualifiedName { get; }

        string MethodName { get; }

        IReadOnlyDictionary<string, object?> Arguments { get; }
    }
}
