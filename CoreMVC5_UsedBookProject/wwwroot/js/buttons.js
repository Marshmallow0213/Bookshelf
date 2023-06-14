$(document).ready(function () {
    var buttonpartial = $('.button-cover');
    var btnLogin = buttonpartial.find('.btn-login');
    var btnLogout = buttonpartial.find('.btn-logout');
    var interval;

    buttonpartial.hover(
        function () {
            clearInterval(interval);
            var self = this;
            interval = setInterval(function () {
                $(self).addClass('hovered');
                btnLogin.stop().animate({ opacity: 1 }, 1000);
                btnLogout.stop().animate({ opacity: 1 }, 1000);
                clearInterval(interval);
            }, 1500);
        },
        function () {
            clearInterval(interval);
            buttonpartial.removeClass('hovered');
            btnLogin.stop().animate({ opacity: 0 }, 500);
            btnLogout.stop().animate({ opacity: 0 }, 500);
        }
    );
});
