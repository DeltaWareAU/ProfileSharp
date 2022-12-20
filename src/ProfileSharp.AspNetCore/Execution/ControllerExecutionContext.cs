using ProfileSharp.Execution.Context;

namespace ProfileSharp.AspNetCore.Execution
{
    public sealed class ControllerExecutionContext : ExecutionContext
    {
        public string RequestPath { get; set; } = null!;
        public string RequestMethod { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            if (!(obj is ControllerExecutionContext context))
            {
                return false;
            }

            if (RequestPath != context.RequestPath || RequestMethod != context.RequestMethod)
            {
                return false;
            }

            if (!base.Equals(obj))
            {
                return false;
            }

            return true;
        }
    }
}
