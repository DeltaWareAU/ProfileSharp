using ProfileSharp.Execution;

namespace ProfileSharp.AspNetCore.Execution
{
    public interface IControllerExecutionStep : IExecutionStep
    {
        string RequestPath { get; }

        string RequestMethod { get; }
    }
}
