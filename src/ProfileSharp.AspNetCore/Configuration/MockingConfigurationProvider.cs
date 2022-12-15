using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.AspNetCore.Filters;
using ProfileSharp.Configuration.Mocking;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.AspNetCore.Configuration
{
    public class MockingConfigurationProvider : IMockingConfigurationProvider
    {
        public void Configure(IServiceCollection services)
        {
            services.AddTransient<MockScopeInitializationFilter>();
            services.Configure<MvcOptions>(o => o.Filters.AddService<MockScopeInitializationFilter>());
        }
    }
}
