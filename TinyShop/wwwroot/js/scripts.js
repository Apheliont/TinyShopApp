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

export function initializeRatings() {
    $('.ui.rating.product').rating();
}

export function initializeFilterRating(dotNetObj, ratingElementId) {
    $(`#${ratingElementId}`).rating({
        onRate: async function(value) {
            return await dotNetObj.invokeMethodAsync('FilterRatingChanged', value);
        }
    });
}

export function setFilterRatingState(ratingElementId, value) {
    $(`#${ratingElementId}`).rating('set rating', value);
}