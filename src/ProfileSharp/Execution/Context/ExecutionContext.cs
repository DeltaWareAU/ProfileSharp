using System.Collections.Generic;

namespace ProfileSharp.Execution.Context
{
    public class ExecutionContext : IExecutionContext
    {
        public string AssemblyQualifiedName { get; set; } = null!;

        public string MethodName { get; set; } = null!;

        public IReadOnlyDictionary<string, object?> Arguments { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (!(obj is ExecutionContext context))
            {
                return false;
            }

            bool basicMatch =
                AssemblyQualifiedName != context.AssemblyQualifiedName ||
                MethodName != context.MethodName ||
                Arguments.Count != context.Arguments.Count;

            if (basicMatch)
            {
                return false;
            }

            return context.ComputeHash() == this.ComputeHash();
        }

        public override int GetHashCode()
             => this.ComputeHash().GetHashCode();
    }
}
