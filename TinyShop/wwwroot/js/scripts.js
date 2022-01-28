export function initializeDropdownMenu() {
    $('.ui.dropdown').dropdown();
}


export function initializeSlider(sliderElementId, dotNetObj, lowerBound, upperBound, lowerValue, upperValue, step) {
    $(`#${sliderElementId}`)
        .slider({
            min: lowerBound,
            max: upperBound,
            start: lowerValue,
            end: upperValue,
            step: step,
            onMove: async function (value, min, max) {
                return await dotNetObj.invokeMethodAsync('SliderChanged', value, min, max);
            }
        });
}

export function setSliderState(sliderElementId, lowerBound, upperBound) {
    $(`#${sliderElementId}`).slider('set rangeValue', lowerBound, upperBound, true);
}


export function initializeAccordion() {
    $('.ui.accordion').accordion();
}

export function showElement(elemntId) {
    $(`#${elemntId}`).slideDown(400);
}

export function hideElement(elemntId) {
    $(`#${elemntId}`).slideUp(400);
}

export function hideElements(cssClass) {
    $(`.${cssClass}`).slideUp(400);
}


export function fadeUpItems(cssClass) {
    makeItemsVisible(cssClass);
    $(`.${cssClass}`)
        .transition('toggle')
        .transition({
            animation: 'fade left',
            reverse: 'auto', // default setting
            interval: 150,
        });
}


export function makeItemsVisible(cssClass) {
    $(`.${cssClass}`).css('visibility', 'visible');
}

export function initializeTab() {
    $('.menu .item').tab();
}

export function fadeInOut(cssClass) {
    $(`.${cssClass}`).transition('fade');
}

export function flash(cssClass) {
    $(`.${cssClass}`).animate({
        zoom: '150%',
        opacity: 0.25,
    },
    300,
    function () {
        $(this).animate({
            zoom: '100%',
            opacity: 1,
        }, 300, function () { })
    })
}

export function initializeRatings() {
    $('.ui.rating.product').rating();
}

export function initializeRatingFilter(dotNetObj, ratingElementId) {
    $(`#${ratingElementId}`).rating({
        onRate: async function (value) {
            return await dotNetObj.invokeMethodAsync('RatingChanged', value);
        }
    });
}

export function resetRating(ratingElementId) {
    $(`#${ratingElementId}`).rating('clear rating');
}



export function setFilterRatingState(ratingElementId, value) {
    $(`#${ratingElementId}`).rating('set rating', value);
}

export function initCarousel() {
    let slideNow = 1;
    let translateWidth = 0;
    var slideInterval = 5000;

    const slideCount = $('#carousel-wrapper').children().length;

    function nextSlide() {
        if (slideNow == slideCount || slideNow <= 0 || slideNow > slideCount) {
            $('#carousel-wrapper').css('transform', 'translate(0, 0)');
            slideNow = 1;
        } else {
            translateWidth = -$('#carousel-viewport').width() * (slideNow);
            $('#carousel-wrapper').css({
                'transform': 'translate(' + translateWidth + 'px, 0)',
                '-webkit-transform': 'translate(' + translateWidth + 'px, 0)',
                '-ms-transform': 'translate(' + translateWidth + 'px, 0)',
            });
            slideNow++;
        }
    }

    $(document).ready(function () {
        setInterval(nextSlide, slideInterval);
    });
}

export function changeItemWidth(itemId, value) {
    const originalWidth = Math.round($(`#${itemId}`).width());
    $(`#${itemId}`).animate({ width: originalWidth + value }, {
        duration: 450,
    });
    return originalWidth;
}

export function setItemWidth(itemId, value) {
    $(`#${itemId}`).animate({ width: value }, {
        duration: 450,
    });
}

