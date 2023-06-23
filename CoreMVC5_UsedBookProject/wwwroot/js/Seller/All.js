/*autoresizing*/
textarea = document.querySelectorAll("textarea");
textarea.forEach(element => {
    autoresizing(element);
});
textarea.forEach(element => {
    element.addEventListener("input", () => {
        autoresizing(element);
    })
});
function autoresizing(element) {
    element.style.height = 'auto';
    element.style.height = element.scrollHeight + 'px';
}
/*top btn*/
let topbutton = document.getElementById("topBtn");
// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 75 || document.documentElement.scrollTop > 75) {
        topbutton.style.display = "block";
    } else {
        topbutton.style.display = "none";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
}
$('body').on('click', function (e) {
    let header = document.querySelector('header');
    if (!header.contains(e.target)) {
        $('#switch-aside').prop("checked", false);
        $('#switch-search').prop("checked", false);
        $('#switch-login').prop("checked", false);
    };
});