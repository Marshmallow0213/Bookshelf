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