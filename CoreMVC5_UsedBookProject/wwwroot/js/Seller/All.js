﻿/*autoresizing*/
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
    let main = document.querySelector("main");
    if (document.body.scrollTop > 75 || document.documentElement.scrollTop > 75) {
        topbutton.style.display = "block";
        main.style.marginTop = "60px";
    } else {
        topbutton.style.display = "none";
        main.style.marginTop = "0px";
    }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
    $("html, body").animate({ scrollTop: 0 }, "slow");
}
// Home Index Search
function getPredictions() {
    var searchText = $('#searchInput').val();
    if (searchText.length > 0) {
        $.ajax({
            url: '/Home/GetPredictions',
            type: 'GET',
            data: { searchText: searchText },
            success: function (data) {
                $('#predictionList').html(data);
            }
        });
    } else {
        $('#predictionList').empty();
    }
}