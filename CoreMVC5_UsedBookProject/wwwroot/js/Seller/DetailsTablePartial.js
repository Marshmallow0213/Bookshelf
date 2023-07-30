//start autoresizing
window.onload = function () {
    textarea = document.querySelectorAll("textarea");
    textarea.forEach(element => {
        autoresizing(element);
    });
}
function autoresizing(element) {
    element.style.height = 'auto';
    element.style.height = element.scrollHeight + 'px';
}
//end