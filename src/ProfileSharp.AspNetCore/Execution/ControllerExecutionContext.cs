using ProfileSharp.Execution.Context;

namespace ProfileSharp.AspNetCore.Execution
{
    public sealed class ControllerExecutionContext : ExecutionContext
    {
        public string RequestPath { get; set; } = null!;
        public string RequestMethod { get; set; } = null!;
    }
}
