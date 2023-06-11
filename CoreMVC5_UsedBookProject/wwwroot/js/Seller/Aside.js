window.addEventListener("resize", function() {
    if (window.innerWidth >= "768") {
        document.querySelector("#menu").style.position = "relative";
        document.querySelector("#menu").style.left = "0";
        document.querySelector("#hamber").style.display = "none";
    } else {
        document.querySelector("#menu").style.transition = ".5s";
        document.querySelector("#menu").style.position = "absolute";
        document.querySelector("#menu").style.left = "-1000px";
        document.querySelector("#hamber").style.display = "block";
    }
});
window.addEventListener("load", function () {
    if (window.innerWidth >= "768") {
        document.querySelector("#menu").style.position = "relative";
        document.querySelector("#menu").style.left = "0";
        document.querySelector("#hamber").style.display = "none";
    } else {
        document.querySelector("#menu").style.position = "absolute";
        document.querySelector("#menu").style.left = "-1000px";
        document.querySelector("#hamber").style.display = "block";
    }
});
document.querySelector("#hamber").addEventListener("click", () => {
    if (document.querySelector("#menu").style.left == "0px") {
        document.querySelector("#menu").style.left = "-1000px";
    } else {
        document.querySelector("#menu").style.left = "0px";
    }
})
document.querySelector("#drop-login-btn").addEventListener("click", () => {
    if (document.querySelector("#drop-login-status").style.maxHeight == "0px") {
        document.querySelector("#drop-login-status").style.maxHeight = "10000px";
        document.querySelector("#drop-login-status").style.borderBottom = "1px solid black";
    } else {
        document.querySelector("#drop-login-status").style.maxHeight = "0px";
        document.querySelector("#drop-login-status").style.borderBottom = "1px solid #80808025";
    }
})