/**
 * Provides AOS (Animate On Scroll) functionality for detecting when elements become visible in the viewport.
 * Designed to work with Blazor via a .NET object reference for callback.
 */
window.aos = {
    /**
     * Initializes the IntersectionObserver to watch for elements entering the viewport.
     * When an observed element becomes visible, invokes the 'OnElementVisible' method on the provided .NET object reference.
     *
     * @param {object} dotNetObjectRef - The .NET object reference used to invoke methods asynchronously from JavaScript.
     */
    init: function(dotNetObjectRef) {},

    /**
     * Registers a DOM element with the IntersectionObserver for visibility tracking.
     *
     * @param {Element} element - The DOM element to observe.
     */
    observer: function(element) {},

    /**
     * Disconnects the IntersectionObserver and stops observing all elements.
     * Call this method to clean up resources when observation is no longer needed.
     */
    dispose: function() {}
};
window.scrollbarservice={
    init:function(dotnetObjectRef){
        this.observer=new IntersectionObserver((entries)=>{
            entries.forEach(entry=>{
                if(entry.isIntersecting&&entry.target.classList.contains("card-item")){
                    const elementId=entry.target.id;
                    dotnetObjectRef.invokeMethodAsync("OnScrollBar",elementId);
                    this.observer(entry.target);
                }
            });
        },{
            threshold:0.1,
        });
    },
    observer:function(element){
        this.observer?.observer(element);
    },
    dispose:function(){
        this.observer?.disconnect();
    }

};