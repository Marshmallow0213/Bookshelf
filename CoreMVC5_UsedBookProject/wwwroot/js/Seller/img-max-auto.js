function imgMaxAuto(element) {
    let width = element.width;
    let height = element.height;
    if (width >= height) {
        element.style.maxWidth = "100%";
    }
    else {
        element.style.maxHeight = "100%";
    }
}