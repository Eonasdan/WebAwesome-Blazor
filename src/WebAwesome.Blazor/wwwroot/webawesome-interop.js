/**
 * Web Awesome Blazor JavaScript Interop Module
 * Provides JavaScript functionality for Web Awesome Blazor components
 */

/**
 * Sets a custom validation message on a Web Awesome form control element
 * @param {HTMLElement} element - The Web Awesome form control element
 * @param {string} message - The validation message to display, or empty string to clear
 */
export function setCustomValidity(element, message) {
    if (!element) {
        throw new Error('Element reference is null or undefined');
    }

    // Verify this is a Web Awesome component that supports setCustomValidity
    const tagName = element.tagName?.toLowerCase();
    const supportedComponents = [
        'wa-button',
        'wa-checkbox',
        'wa-color-picker',
        'wa-input',
        'wa-radio',
        'wa-radio-group',
        'wa-select',
        'wa-slider',
        'wa-switch',
        'wa-textarea'
    ];

    if (!supportedComponents.includes(tagName)) {
        throw new Error(`setCustomValidity is not supported on ${tagName || 'unknown'} elements`);
    }

    // Check if the element has the setCustomValidity method
    if (typeof element.setCustomValidity !== 'function') {
        throw new Error(`Element ${tagName} does not support setCustomValidity method. Ensure Web Awesome library is properly loaded.`);
    }

    try {
        element.setCustomValidity(message || '');
    } catch (error) {
        throw new Error(`Failed to set custom validity on ${tagName}: ${error.message}`);
    }
}