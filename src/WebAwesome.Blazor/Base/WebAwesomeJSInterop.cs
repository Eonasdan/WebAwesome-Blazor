using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace WebAwesome.Blazor.Base;

/// <summary>
/// Service for Web Awesome JavaScript interop operations
/// </summary>
public class WebAwesomeJSInterop
{
    private readonly IJSRuntime jsRuntime;
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public WebAwesomeJSInterop(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        moduleTask = new Lazy<Task<IJSObjectReference>>(() => LoadModuleAsync());
    }

    /// <summary>
    /// Sets a custom validation message on a Web Awesome form control element
    /// </summary>
    /// <param name="elementReference">Reference to the form control element</param>
    /// <param name="message">The validation message to display, or empty string to clear</param>
    /// <returns>A task that represents the asynchronous operation</returns>
    public async Task SetCustomValidityAsync(ElementReference elementReference, string message)
    {
        if (elementReference.Id == null)
            throw new ArgumentException("Element reference is not valid", nameof(elementReference));

        try
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("setCustomValidity", elementReference, message ?? string.Empty);
        }
        catch (JSException ex)
        {
            throw new InvalidOperationException($"Failed to set custom validity: {ex.Message}", ex);
        }
        catch (JSDisconnectedException)
        {
            // JS runtime is disconnected, ignore silently
        }
    }

    /// <summary>
    /// Disposes the JavaScript module reference
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }

    private async Task<IJSObjectReference> LoadModuleAsync()
    {
        return await jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/WebAwesome.Blazor/webawesome-interop.js");
    }
}