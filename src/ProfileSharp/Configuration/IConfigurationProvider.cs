using Microsoft.Extensions.DependencyInjection;

namespace ProfileSharp.Configuration
{
    public interface IConfigurationProvider
    {
        void Configure(IServiceCollection services);
    }
}
