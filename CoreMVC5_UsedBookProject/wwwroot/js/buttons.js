$(document).ready(function () {
    var buttonPartial = $('.button-cover');
    var btnLogin = buttonPartial.find('.btn-hide');
    var btnLogout = buttonPartial.find('.btn-logout');
    var timeoutIn;
    var timeoutOut;
    var animationDurationIn = 500;
    var animationDurationOut = 300;

    function showAnimation() {
        buttonPartial.addClass('hovered');
        btnLogin.stop().animate({ opacity: 1 }, animationDurationIn);
        btnLogout.stop().animate({ opacity: 1 }, animationDurationIn);
    }

    function hideAnimation() {
        buttonPartial.removeClass('hovered');
        btnLogin.stop().animate({ opacity: 0 }, animationDurationOut);
        btnLogout.stop().animate({ opacity: 0 }, animationDurationOut);
    }

    buttonPartial.hover(
        function () {
            clearTimeout(timeoutOut);
            timeoutIn = setTimeout(function () {
                showAnimation();
            }, 1500);
        },
        function () {
            clearTimeout(timeoutIn);
            timeoutOut = setTimeout(function () {
                hideAnimation();
            }, 500);
        }
    );

    // 初始化隐藏按钮
    hideAnimation();
});
