let reload = document.querySelectorAll(".reload-by-datetime");
reload.forEach(element => {
    var d = new Date();
    var strDate = `${d.getFullYear()}${(d.getMonth() + 1).toString().padStart(2, 0)}${d.getDate().toString().padStart(2, 0)}${d.getHours().toString().padStart(2, 0)}${d.getMinutes().toString().padStart(2, 0)}${d.getSeconds().toString().padStart(2, 0) }`;
    let src = `${element.getAttribute("src")}?v=${strDate}`;
    element.setAttribute("src", src);
})