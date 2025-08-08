export function initializeCarousel(dotNetHelper, cardList, container) {
    // Mobile detection
    const mobileMediaQuery = window.matchMedia('(max-width: 768px)');
    handleMobileChange(mobileMediaQuery);
    mobileMediaQuery.addListener(handleMobileChange);

    // Intersection Observer for scroll detection
    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const cardNo = parseInt(entry.target.getAttribute('data-card-id'));
                dotNetHelper.invokeMethodAsync('OnCardScrolled', cardNo);
            }
        });
    }, {
        threshold: 0.6,
        root: cardList
    });

    // Observe all cards
    document.querySelectorAll('.card-item').forEach(card => {
        observer.observe(card);
    });

    // Store for cleanup
    window.carouselObserver = observer;
    window.carouselMobileQuery = mobileMediaQuery;

    function handleMobileChange(e) {
        dotNetHelper.invokeMethodAsync('OnMobileDetected', e.matches);
    }
}

export function disposeCarousel() {
    if (window.carouselObserver) {
        window.carouselObserver.disconnect();
    }
    if (window.carouselMobileQuery) {
        window.carouselMobileQuery.removeListener(handleMobileChange);
    }
}

export function scrollToElement(element) {
    element.scrollIntoView({
        behavior: 'smooth',
        block: 'nearest',
        inline: 'center'
    });
}