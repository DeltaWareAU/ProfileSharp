using ProfileSharp.Execution.Context;

namespace ProfileSharp.NServiceBus.Context
{
    public sealed class HandleMessageExecutionContext : ExecutionContext
    {
        public override bool Equals(object? obj)
        {
            return obj is HandleMessageExecutionContext && base.Equals(obj);
        }
    }
}
