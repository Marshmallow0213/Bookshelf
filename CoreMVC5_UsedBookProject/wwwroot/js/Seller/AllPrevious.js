function productImageError(element, id, productId, image) {
    if (image == "無圖片") {
        element.setAttribute('onerror', 'this.classList.add("img200pxEmpty");');
        element.src = `/deafultpictures/deafultbookpicture.jpg`;
    }
    else {
        element.setAttribute('onerror', 'this.classList.add("img200pxEmpty");');
        element.src = `/Images/Users/${id}/Products/${productId}/${image}`;
    }
}
function userImageError(element) {
    element.src = '/DeafultPictures/EmptyUserIcon.png';
    element.setAttribute('onerror', '');
}
function carouselImageError(element) {
    element.src = '/DeafultPictures/Carousel.jpg';
    element.setAttribute('onerror', '');
}
function carouselImageErrorSecond(element) {
    element.src = '/DeafultPictures/CarouselSecond.jpg';
    element.setAttribute('onerror', '');
}
function carouselImageErrorThird(element) {
    element.src = '/DeafultPictures/CarouselThird.jpg';
    element.setAttribute('onerror', '');
}