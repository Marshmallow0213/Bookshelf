﻿function productImageError(element, id, productId, image) {
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
    element.src = `/deafultpictures/emptyusericon.png`;
    element.setAttribute('onerror', '');
    let id = document.getElementById('userId');
    var url = `/Home/ErrorUserImages?id=${id.value}`;
    $.ajax({
        type: "POST",
        url: url,
        error: function (xhr, status, error) { },
        success: function (data) {
        }
    });
}
function carouselImageError(element) {
    element.src = '/DeafultPictures/Carousel.jpg';
    element.setAttribute('onerror', '');
    var url = `/Home/ErrorCarouselImages`;
    $.ajax({
        type: "POST",
        url: url,
        error: function (xhr, status, error) { },
        success: function (data) {
        }
    });
}
function carouselImageErrorSecond(element) {
    element.src = '/DeafultPictures/CarouselSecond.jpg';
    element.setAttribute('onerror', '');
    var url = `/Home/ErrorCarouselImages`;
    $.ajax({
        type: "POST",
        url: url,
        error: function (xhr, status, error) { },
        success: function (data) {
        }
    });
}
function carouselImageErrorThird(element) {
    element.src = '/DeafultPictures/CarouselThird.jpg';
    element.setAttribute('onerror', '');
    var url = `/Home/ErrorCarouselImages`;
    $.ajax({
        type: "POST",
        url: url,
        data: id,
        error: function (xhr, status, error) { },
        success: function (data) {
        }
    });
}