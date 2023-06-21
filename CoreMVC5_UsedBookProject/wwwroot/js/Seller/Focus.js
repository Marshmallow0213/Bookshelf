textarea = document.querySelectorAll("textarea");
textarea.forEach(element => {
    autoresizing(element);
});
//start autoresizing
textarea.forEach(element => {
    element.addEventListener("input", () => {
        autoresizing(element);
    })
});
function autoresizing(element) {
    element.style.height = 'auto';
    element.style.height = element.scrollHeight + 'px';
}
//end