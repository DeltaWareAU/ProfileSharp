using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using ProfileSharp.AspNetCore.Mocking;

// ReSharper disable once CheckNamespace
namespace ProfileSharp.Mocking.Configuration
{
    public static class MockingOptionsBuilderExtensions
    {
        public static void AddControllers(this IMockingConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Services.AddTransient<ControllerMockInitializer>();
            configurationBuilder.Services.Configure<MvcOptions>(o => o.Filters.AddService<ControllerMockInitializer>());
        }
    }
}
