window.addEventListener("resize", function() {
    if (window.innerWidth >= "768") {
        document.querySelector("#hamber").style.display = "none";
        document.querySelector("#aside_main").style.display = "flex";
        if ($('.aside-menu').hasClass('aside-menu-open')) {
            $('.aside-menu').toggleClass('aside-menu-open');
        }
    } else {
        document.querySelector("#hamber").style.display = "flex";
        document.querySelector("#aside_main").style.display = "none";
    }
});
window.addEventListener("load", function () {
    if (window.innerWidth >= "768") {
        document.querySelector("#hamber").style.display = "none";
        document.querySelector("#aside_main").style.display = "flex";
    } else {
        document.querySelector("#hamber").style.display = "flex";
        document.querySelector("#aside_main").style.display = "none";
    }
});
$('body').on('click', function (e) {
    if (!$('.no-hide').is(e.target)
    ) {
        if ($('.aside-menu').hasClass('aside-menu-open')) {
            $('.aside-menu').toggleClass('aside-menu-open');
        }
        if ($('.login-menu').hasClass('login-menu-open')) {
            $('.login-menu').toggleClass('login-menu-open');
        }
    };
    console.log(e.target);
});
$('#hamber').on('click', function (event) {
    $('.aside-menu').toggleClass('aside-menu-open');
});
$('#login-btn').on('click', function (event) {
    $('.login-menu').toggleClass('login-menu-open');
});