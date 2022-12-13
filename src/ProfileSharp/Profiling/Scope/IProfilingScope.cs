using ProfileSharp.Execution;

namespace ProfileSharp.Profiling.Scope
{
    public interface IProfilingScope
    {
        void RegisterStep(IExecutionStep step);
    }
}
