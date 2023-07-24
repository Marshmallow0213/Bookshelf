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