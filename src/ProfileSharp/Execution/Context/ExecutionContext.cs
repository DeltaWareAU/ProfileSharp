using System.Collections.Generic;

namespace ProfileSharp.Execution.Context
{
    public class ExecutionContext : IExecutionContext
    {
        public string AssemblyQualifiedName { get; set; } = null!;

        public string MethodName { get; set; } = null!;

        public IReadOnlyDictionary<string, object> Arguments { get; set; } = null!;
    }
}
