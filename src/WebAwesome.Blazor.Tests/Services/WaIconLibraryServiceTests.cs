using System;
using System.Threading.Tasks;
using WebAwesome.Blazor.Services;
using WebAwesome.Blazor.Base;
using WebAwesome.Blazor.Models;
using Xunit;
using NSubstitute;

namespace WebAwesome.Blazor.Tests.Services;

/// <summary>
/// Unit tests for WaIconLibraryService
/// </summary>
public class WaIconLibraryServiceTests
{
    private readonly WebAwesomeJSInterop mockJSInterop;
    private readonly WaIconLibraryService service;

    public WaIconLibraryServiceTests()
    {
        mockJSInterop = Substitute.For<WebAwesomeJSInterop>(Substitute.For<Microsoft.JSInterop.IJSRuntime>());
        service = new WaIconLibraryService(mockJSInterop);
    }

    [Fact]
    public void Constructor_WithNullJSInterop_ThrowsArgumentNullException()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new WaIconLibraryService(null!));
    }

    [Fact]
    public async Task RegisterFontAwesomeProAsync_WithValidKitCode_CallsJSInterop()
    {
        // Arrange
        const string kitCode = "test-kit-123";

        // Act
        await service.RegisterFontAwesomeProAsync(kitCode);

        // Assert
        await mockJSInterop.Received(1).RegisterIconLibraryAsync("fa-pro", Arg.Is<IconLibraryOptions>(
            opts => opts.Resolver == $"https://kit.fontawesome.com/{kitCode}/{{family}}/{{variant}}/{{name}}.svg"));
    }

    [Fact]
    public async Task RegisterFontAwesomeProAsync_WithNullKitCode_ThrowsArgumentNullException()
    {
        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            service.RegisterFontAwesomeProAsync(null!));
    }

    [Fact]
    public async Task RegisterHeroiconsAsync_WithDefaultVersion_CallsJSInterop()
    {
        // Act
        await service.RegisterHeroiconsAsync();

        // Assert
        await mockJSInterop.Received(1).RegisterIconLibraryAsync("heroicons", Arg.Is<IconLibraryOptions>(
            opts => opts.Resolver == "https://cdn.jsdelivr.net/npm/heroicons@2.0.18/24/{variant}/{name}.svg"));
    }

    [Fact]
    public async Task RegisterLucideAsync_WithCustomVersion_CallsJSInterop()
    {
        // Arrange
        const string version = "0.294.0";

        // Act
        await service.RegisterLucideAsync(version);

        // Assert
        await mockJSInterop.Received(1).RegisterIconLibraryAsync("lucide", Arg.Is<IconLibraryOptions>(
            opts => opts.Resolver == $"https://cdn.jsdelivr.net/npm/lucide@{version}/icons/{{name}}.svg"));
    }

    [Fact]
    public async Task RegisterIconLibraryAsync_CallsUnderlyingJSInterop()
    {
        // Arrange
        const string name = "custom-lib";
        var options = new IconLibraryOptions { Resolver = "https://example.com/{name}.svg" };

        // Act
        await service.RegisterIconLibraryAsync(name, options);

        // Assert
        await mockJSInterop.Received(1).RegisterIconLibraryAsync(name, options);
    }

    [Fact]
    public async Task UnregisterIconLibraryAsync_CallsUnderlyingJSInterop()
    {
        // Arrange
        const string name = "custom-lib";

        // Act
        await service.UnregisterIconLibraryAsync(name);

        // Assert
        await mockJSInterop.Received(1).UnregisterIconLibraryAsync(name);
    }

    [Fact]
    public async Task SetDefaultIconFamilyAsync_CallsUnderlyingJSInterop()
    {
        // Arrange
        const string family = "sharp";

        // Act
        await service.SetDefaultIconFamilyAsync(family);

        // Assert
        await mockJSInterop.Received(1).SetDefaultIconFamilyAsync(family);
    }

    [Fact]
    public async Task GetDefaultIconFamilyAsync_CallsUnderlyingJSInterop()
    {
        // Arrange
        const string expectedFamily = "classic";
        mockJSInterop.GetDefaultIconFamilyAsync().Returns(expectedFamily);

        // Act
        var result = await service.GetDefaultIconFamilyAsync();

        // Assert
        Assert.Equal(expectedFamily, result);
        await mockJSInterop.Received(1).GetDefaultIconFamilyAsync();
    }
}