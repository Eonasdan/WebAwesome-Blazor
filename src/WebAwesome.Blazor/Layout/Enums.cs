using System;

namespace WebAwesome.Blazor.Layout;

/// <summary>
/// Gap size tokens for Web Awesome gap utilities (wa-gap-*).
/// Based on Web Awesome spacing scale: 0, 3xs, 2xs, xs, s, m, l, xl, 2xl, 3xl.
/// </summary>
public enum GapSize
{
    /// <summary>No gap (wa-gap-0)</summary>
    Gap0,
    /// <summary>3X small gap (wa-gap-3xs)</summary>
    Gap3Xs,
    /// <summary>2X small gap (wa-gap-2xs)</summary>
    Gap2Xs,
    /// <summary>Extra small gap (wa-gap-xs)</summary>
    GapXs,
    /// <summary>Small gap (wa-gap-s)</summary>
    GapS,
    /// <summary>Medium gap (wa-gap-m)</summary>
    GapM,
    /// <summary>Large gap (wa-gap-l)</summary>
    GapL,
    /// <summary>Extra large gap (wa-gap-xl)</summary>
    GapXl,
    /// <summary>2X large gap (wa-gap-2xl)</summary>
    Gap2Xl,
    /// <summary>3X large gap (wa-gap-3xl)</summary>
    Gap3Xl
}

/// <summary>
/// Alignment tokens for Web Awesome align-items utilities (wa-align-items-*).
/// Based on CSS flexbox align-items values.
/// </summary>
public enum AlignItems
{
    /// <summary>Align to flex-start (wa-align-items-start)</summary>
    Start,
    /// <summary>Align to flex-end (wa-align-items-end)</summary>
    End,
    /// <summary>Align to center (wa-align-items-center)</summary>
    Center,
    /// <summary>Stretch to fill container (wa-align-items-stretch)</summary>
    Stretch,
    /// <summary>Align to baseline (wa-align-items-baseline)</summary>
    Baseline
}

/// <summary>
/// Direction tokens for Split layout utility.
/// </summary>
public enum SplitDirection
{
    /// <summary>Horizontal split layout (wa-split or wa-split:row)</summary>
    Row,
    /// <summary>Vertical split layout (wa-split:column)</summary>
    Column
}

/// <summary>
/// Aspect ratio tokens for Frame layout utility.
/// </summary>
public enum FrameAspectRatio
{
    /// <summary>Square aspect ratio (wa-frame:square)</summary>
    Square,
    /// <summary>Landscape aspect ratio (wa-frame:landscape)</summary>
    Landscape,
    /// <summary>Portrait aspect ratio (wa-frame:portrait)</summary>
    Portrait
}

/// <summary>
/// Position tokens for Flank layout utility.
/// </summary>
public enum FlankPosition
{
    /// <summary>Flank positioned at start (wa-flank:start or default)</summary>
    Start,
    /// <summary>Flank positioned at end (wa-flank:end)</summary>
    End
}

/// <summary>
/// Text size tokens for Web Awesome text utilities.
/// </summary>
public enum TextSize
{
    /// <summary>Extra small text (wa-body-xs, wa-heading-xs, wa-caption-xs)</summary>
    Xs,
    /// <summary>Small text (wa-body-s, wa-heading-s, wa-caption-s)</summary>
    S,
    /// <summary>Medium text (wa-body-m, wa-heading-m, wa-caption-m)</summary>
    M,
    /// <summary>Large text (wa-body-l, wa-heading-l, wa-caption-l)</summary>
    L,
    /// <summary>Extra large text (wa-body-xl, wa-heading-xl, wa-caption-xl)</summary>
    Xl,
    /// <summary>2X large text (wa-heading-2xl)</summary>
    Xl2,
    /// <summary>3X large text (wa-heading-3xl)</summary>
    Xl3
}

/// <summary>
/// Text style variants for Web Awesome text utilities.
/// </summary>
public enum TextVariant
{
    /// <summary>Body text styling (wa-body-*)</summary>
    Body,
    /// <summary>Heading text styling (wa-heading-*)</summary>
    Heading,
    /// <summary>Caption text styling (wa-caption-*)</summary>
    Caption
}

#region ------ Extension Methods ------

/// <summary>
/// Extension methods for converting Web Awesome layout enums to HTML attribute values
/// </summary>
public static class WaLayoutEnumExtensions
{
    /// <summary>
    /// Converts GapSize to HTML class suffix
    /// </summary>
    public static string ToHtmlValue(this GapSize gap)
    {
        return gap switch
        {
            GapSize.Gap0 => "0",
            GapSize.Gap3Xs => "3xs",
            GapSize.Gap2Xs => "2xs",
            GapSize.GapXs => "xs",
            GapSize.GapS => "s",
            GapSize.GapM => "m",
            GapSize.GapL => "l",
            GapSize.GapXl => "xl",
            GapSize.Gap2Xl => "2xl",
            GapSize.Gap3Xl => "3xl",
            _ => throw new ArgumentOutOfRangeException(nameof(gap), gap, null)
        };
    }

    /// <summary>
    /// Converts AlignItems to HTML class suffix
    /// </summary>
    public static string ToHtmlValue(this AlignItems align)
    {
        return align switch
        {
            AlignItems.Start => "start",
            AlignItems.End => "end",
            AlignItems.Center => "center",
            AlignItems.Stretch => "stretch",
            AlignItems.Baseline => "baseline",
            _ => throw new ArgumentOutOfRangeException(nameof(align), align, null)
        };
    }

    /// <summary>
    /// Converts SplitDirection to HTML class suffix
    /// </summary>
    public static string ToHtmlValue(this SplitDirection direction)
    {
        return direction switch
        {
            SplitDirection.Row => "row",
            SplitDirection.Column => "column",
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    /// <summary>
    /// Converts FrameAspectRatio to HTML class suffix
    /// </summary>
    public static string ToHtmlValue(this FrameAspectRatio ratio)
    {
        return ratio switch
        {
            FrameAspectRatio.Square => "square",
            FrameAspectRatio.Landscape => "landscape",
            FrameAspectRatio.Portrait => "portrait",
            _ => throw new ArgumentOutOfRangeException(nameof(ratio), ratio, null)
        };
    }

    /// <summary>
    /// Converts FlankPosition to HTML class suffix
    /// </summary>
    public static string ToHtmlValue(this FlankPosition position)
    {
        return position switch
        {
            FlankPosition.Start => "start",
            FlankPosition.End => "end",
            _ => throw new ArgumentOutOfRangeException(nameof(position), position, null)
        };
    }

    /// <summary>
    /// Converts TextSize to HTML class suffix
    /// </summary>
    public static string ToHtmlValue(this TextSize size)
    {
        return size switch
        {
            TextSize.Xs => "xs",
            TextSize.S => "s",
            TextSize.M => "m",
            TextSize.L => "l",
            TextSize.Xl => "xl",
            TextSize.Xl2 => "2xl",
            TextSize.Xl3 => "3xl",
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, null)
        };
    }

    /// <summary>
    /// Converts TextVariant to HTML class prefix
    /// </summary>
    public static string ToHtmlValue(this TextVariant variant)
    {
        return variant switch
        {
            TextVariant.Body => "body",
            TextVariant.Heading => "heading",
            TextVariant.Caption => "caption",
            _ => throw new ArgumentOutOfRangeException(nameof(variant), variant, null)
        };
    }
}

#endregion
