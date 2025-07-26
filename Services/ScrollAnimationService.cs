/// <summary>
/// Provides a service for integrating scroll-based animations with JavaScript interop in a Blazor application.
/// This service allows registering callbacks for specific elements that should trigger when they become visible
/// in the viewport, leveraging a JavaScript animation library (such as AOS).
/// </summary>
/// <remarks>
/// The <see cref="ScrollAnimationService"/> manages the lifecycle of the JavaScript animation library,
/// registers .NET callbacks for element visibility events, and ensures proper disposal of resources.
/// </remarks>
using Microsoft.JSInterop;

public class ScrollAnimationService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private DotNetObjectReference<ScrollAnimationService>? _dotNetObjectRef;
    private readonly Dictionary<string, Action> _animationCallback = new();
    public ScrollAnimationService(IJSRuntime jSRuntime)
    {
        _jsRuntime = jSRuntime;
    }
    public async Task InitializeAsync()
    {
        _dotNetObjectRef = DotNetObjectReference.Create(this);
        await _jsRuntime.InvokeVoidAsync("aos.init", _dotNetObjectRef);
    }
    public void RegisterAnimation(string elementId, Action callback)
    {
        _animationCallback[elementId] = callback;
    }
    [JSInvokable]
    public void OnElementVisible(string elementId)
    {
        if (_animationCallback.TryGetValue(elementId, out var callback))
        {
            callback.Invoke();
        }
    }
    public async ValueTask DisposeAsync()
    {
        _dotNetObjectRef?.Dispose();
        await _jsRuntime.InvokeVoidAsync("aos.dispose");
    }
}