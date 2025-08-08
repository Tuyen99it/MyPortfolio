using Microsoft.JSInterop;
public class ScrollBarItemService : IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime;
    private DotNetObjectReference<ScrollBarItemService>? _dotnetObjectRef;
    private readonly Dictionary<string, Action> _scrollBarCallback = new();
    public ScrollBarItemService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    public async Task InitializeAsync()
    {
        _dotnetObjectRef = DotNetObjectReference.Create(this);
        await _jsRuntime.InvokeVoidAsync("scrollbarservice.init", _dotnetObjectRef);
    }
    public void RegiserScrollBar(string elementId, Action callback)
    {
        _scrollBarCallback[elementId] = callback;
    }
    [JSInvokable]
    public void OnScrollItem(string elementId)
    {
        if (_scrollBarCallback.TryGetValue(elementId, out var callback))
        {
            callback.Invoke();
        }
    }
    public async ValueTask DisposeAsync()
    {
        _dotnetObjectRef?.Dispose();
        await _jsRuntime.InvokeVoidAsync("scrollbarservice.dispose");
    }
 }