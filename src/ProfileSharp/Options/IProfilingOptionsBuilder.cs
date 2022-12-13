using Microsoft.Extensions.DependencyInjection;

namespace ProfileSharp.Options
{
    public interface IProfilingOptionsBuilder
    {
        IServiceCollection Services { get; }
    }
}
