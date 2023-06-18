let reload = document.querySelectorAll("img");
reload.forEach(element => {
    var d = new Date();
    var strDate = `${d.getFullYear()}${(d.getMonth() + 1).toString().padStart(2, 0)}${d.getDate().toString().padStart(2, 0)}${d.getHours().toString().padStart(2, 0)}${d.getMinutes().toString().padStart(2, 0)}${d.getSeconds().toString().padStart(2, 0) }`;
    let src = `${element.getAttribute("src")}?v=${strDate}`;
    element.setAttribute("src", src);
})

$('img').bind('error', function () {
    console.log('error');
    $(this).hide().after('<div class="m-auto"><i class="bi bi-images text-center m-auto d-block" style="font-size: 50px;"></i><p>No image</p></div>');
});