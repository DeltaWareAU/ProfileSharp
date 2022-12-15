using System.Collections.Generic;
using System.Linq;

namespace ProfileSharp.Execution.Context
{
    public class ExecutionContext : IExecutionContext
    {
        public string AssemblyQualifiedName { get; set; } = null!;

        public string MethodName { get; set; } = null!;

        public IReadOnlyDictionary<string, object> Arguments { get; set; } = null!;

        public override bool Equals(object obj)
        {
            if (!(obj is IExecutionContext context))
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

            var sourceArguments = Arguments.ToArray();
            var compareArguments = context.Arguments.ToArray();

            for (int i = 0; i < sourceArguments.Length; i++)
            {
                var source = sourceArguments[i];
                var compare = compareArguments[i];

                if (source.Key != compare.Key)
                {
                    return false;
                }

                if (source.Value == null)
                {
                    if (compare.Value != null)
                    {
                        return false;
                    }

                    continue;
                }

                if (source.Value.GetType().IsPrimitive && source.Value.ToString() != compare.Value.ToString())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
