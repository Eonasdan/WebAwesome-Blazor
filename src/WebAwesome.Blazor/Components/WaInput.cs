using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using WebAwesome.Blazor.Base;

namespace WebAwesome.Blazor.Components;

/// <summary>
/// A single-line input component for editing <see cref="string"/> values.
/// Corresponds to the wa-input Web Awesome component.
/// </summary>
public class WaInput : WaInputBase<string?>
{
    private string? _lastRenderedValue;
    private bool _pendingAutoClear;


    #region ------ Visual & Behavior Properties ------

    [Parameter] public string? Placeholder { get; set; }
    [Parameter] public WaInputType Type { get; set; } = WaInputType.Text;
    [Parameter] public WaInputAppearance? Appearance { get; set; }
    [Parameter] public bool Pill { get; set; }
    [Parameter] public bool WithClear { get; set; }
    [Parameter] public bool PasswordToggle { get; set; }
    [Parameter] public bool? Spellcheck { get; set; }

    // Input-specific validation
    [Parameter] public string? Pattern { get; set; }
    [Parameter] public decimal? Min { get; set; }
    [Parameter] public decimal? Max { get; set; }
    [Parameter] public decimal? Step { get; set; }

    #endregion

    #region ------ Events ------

    [Parameter] public EventCallback OnClear { get; set; }
    [Parameter] public EventCallback OnPasswordToggle { get; set; }
    [Parameter] public EventCallback<bool> OnPasswordVisibilityChange { get; set; }

    #endregion

    #region ------ Slots ------

    /// <summary>
    /// Content to display at the start of the input
    /// </summary>
    [Parameter] public RenderFragment? StartContent { get; set; }

    /// <summary>
    /// Content to display at the end of the input
    /// </summary>
    [Parameter] public RenderFragment? EndContent { get; set; }

    #endregion

    #region ------ Overrides ------

    /// <inheritdoc />
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "wa-input");

        // Add common attributes
        var sequence = AddCommonAttributes(builder, 1);

        // Add input-specific attributes
        builder.AddAttributeIfNotNullOrEmpty(20, "placeholder", Placeholder);
        builder.AddAttribute(21, "type", Type.ToHtmlValue());
        builder.AddAttributeIfNotNull(22, "appearance", Appearance?.ToHtmlValue());
        builder.AddAttribute(23, "pill", Pill);
        builder.AddAttribute(24, "with-clear", WithClear);
        builder.AddAttribute(25, "password-toggle", PasswordToggle);
        builder.AddAttributeIfNotNull(26, "spellcheck", Spellcheck);
        builder.AddAttributeIfNotNullOrEmpty(27, "pattern", Pattern);
        builder.AddAttributeIfNotNull(28, "min", Min);
        builder.AddAttributeIfNotNull(29, "max", Max);
        builder.AddAttributeIfNotNull(30, "step", Step);

        // Add value binding
        builder.AddAttribute(31, "value", CurrentValueAsString);
        builder.AddAttribute(32, "onchange", EventCallback.Factory.CreateBinder<string?>(this, value => CurrentValueAsString = value, CurrentValueAsString));
        builder.SetUpdatesAttributeName("value");

        // Add common event handlers
        AddCommonEventHandlers(builder, 40);

        // Add input-specific event handlers
        if (OnClear.HasDelegate)
            builder.AddAttribute(50, "wa-clear", OnClear);

        if (OnPasswordToggle.HasDelegate)
            builder.AddAttribute(51, "wa-password-toggle", OnPasswordToggle);

        if (OnPasswordVisibilityChange.HasDelegate)
            builder.AddAttribute(52, "wa-password-visibility-change", OnPasswordVisibilityChange);

        // Add element reference capture
        builder.AddElementReferenceCapture(53, inputReference => Element = inputReference);

        // Add start slot content
        if (StartContent is not null)
        {
            builder.OpenElement(60, "span");
            builder.AddAttribute(61, "slot", "start");
            builder.AddContent(62, StartContent);
            builder.CloseElement();
        }

        // Add end slot content
        if (EndContent is not null)
        {
            builder.OpenElement(65, "span");
            builder.AddAttribute(66, "slot", "end");
            builder.AddContent(67, EndContent);
            builder.CloseElement();
        }

        // Add label and hint slots
        AddLabelAndHintSlots(builder, 70);

        builder.CloseElement();
    }

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, out string? result, [NotNullWhen(false)] out string? validationErrorMessage)
    {
        result = value;
        validationErrorMessage = null;
        return true;
    }

    protected override Task OnParametersSetAsync()
    {
        // Detect transition: non-empty -> empty
        var current = CurrentValueAsString ?? string.Empty;
        var previous = _lastRenderedValue ?? string.Empty;

        if (previous.Length > 0 && current.Length == 0)
        {
            _pendingAutoClear = true;
        }

        return base.OnParametersSetAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        _lastRenderedValue = CurrentValueAsString ?? string.Empty;

        if (_pendingAutoClear)
        {
            _pendingAutoClear = false;

            if (Element is not null)
            {
                // Ensure the underlying web component's internal state is cleared too.
                await JSInterop.SetValueAndNotifyAsync(Element.Value, string.Empty, dispatchInput: true, dispatchChange: false);
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    public async Task ClearAsync()
    {
        if (Element is null)
            throw new InvalidOperationException("Cannot clear before the component is rendered. Element reference is null.");

        await JSInterop.SetValueAndNotifyAsync(Element.Value, string.Empty, dispatchInput: true, dispatchChange: false);
    }

    #endregion
}
