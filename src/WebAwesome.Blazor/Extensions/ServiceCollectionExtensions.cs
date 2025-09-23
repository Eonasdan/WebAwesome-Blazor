using Microsoft.Extensions.DependencyInjection;
using WebAwesome.Blazor.Base;

namespace WebAwesome.Blazor.Extensions;

/// <summary>
/// Extension methods for configuring Web Awesome Blazor services
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Web Awesome Blazor services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to</param>
    /// <returns>The IServiceCollection so that additional calls can be chained</returns>
    /// <remarks>
    /// This method registers the WebAwesomeJSInterop service which is required for
    /// JavaScript interop functionality such as setCustomValidity() on form controls.
    /// </remarks>
    public static IServiceCollection AddWebAwesome(this IServiceCollection services)
    {
        services.AddSingleton<WebAwesomeJSInterop>();
        return services;
    }
}