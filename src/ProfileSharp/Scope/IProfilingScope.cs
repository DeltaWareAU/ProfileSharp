using ProfileSharp.Execution;

namespace ProfileSharp.Scope
{
    public interface IProfilingScope
    {
        void RegisterStep(IExecutionStep step);
    }
}
