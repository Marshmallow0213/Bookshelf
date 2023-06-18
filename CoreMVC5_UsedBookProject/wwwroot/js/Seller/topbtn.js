// Get the button:
let topbutton = document.getElementById("topBtn");
let navbar = document.getElementById("navbar");
// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function () { scrollFunction() };

function scrollFunction() {
    if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
        topbutton.style.display = "block";
        navbar.style.position = "fixed";
    } else {
        topbutton.style.display = "none";
        navbar.style.position = "relative";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
}