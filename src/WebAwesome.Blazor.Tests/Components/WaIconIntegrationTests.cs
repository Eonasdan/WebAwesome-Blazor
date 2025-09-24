using System;
using System.Threading.Tasks;
using WebAwesome.Blazor.Components;
using Xunit;

namespace WebAwesome.Blazor.Tests.Components;

/// <summary>
/// Integration tests for WaIcon component focusing on icon library functionality
/// </summary>
public class WaIconIntegrationTests
{
    [Fact]
    public async Task SetDefaultIconFamilyAsync_WithNullIconLibraryService_ThrowsNullReferenceException()
    {
        // Arrange
        var component = new WaIcon();
        // IconLibraryService is not injected in test scenario

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
            component.SetDefaultIconFamilyAsync("classic"));
    }

    [Fact]
    public async Task GetDefaultIconFamilyAsync_WithNullIconLibraryService_ThrowsNullReferenceException()
    {
        // Arrange
        var component = new WaIcon();
        // IconLibraryService is not injected in test scenario

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() =>
            component.GetDefaultIconFamilyAsync());
    }
}