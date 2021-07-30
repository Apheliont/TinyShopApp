export function initializeDropdownMenu() {
    $('.ui.dropdown').dropdown();
}


export function initializeSlider(dotNetObj, lowerBound, upperBound, lowerValue, upperValue, step) {
    $('.ui.range.slider')
        .slider({
            min: lowerBound,
            max: upperBound,
            start: lowerValue,
            end: upperValue,
            step: step,
            onMove: function (value, min, max) {
                return dotNetObj.invokeMethodAsync('SliderChanged', value, min, max);
            }
        });
}

export function setSliderState(lowerBound, upperBound) {
    $('.ui.range.slider').slider('set rangeValue', lowerBound, upperBound, true);
}


export function initializeAccordion() {
    $('.ui.accordion')
        .accordion();
}

