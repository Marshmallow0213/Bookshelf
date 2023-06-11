let focusInput = document.querySelectorAll("input");
focusInput.forEach(element => {
    element.addEventListener("focus", () => {
        element.style.border = "1px solid green";
    })
    element.addEventListener("blur", () => {
        element.style.border = "1px solid #80808050";
    })
});
let focustextarea = document.querySelectorAll("textarea");
focustextarea.forEach(element => {
    element.addEventListener("focus", () => {
        element.style.border = "1px solid green";
    })
    element.addEventListener("blur", () => {
        element.style.border = "1px solid #80808050";
    })
});