$(document).ready(function () {
    $('tr').on('click', '.btn-info', function () {
        var row = $(this).closest('tr');
        row.removeClass('first-level-admin second-level-admin');

        if ($(this).hasClass('btn-primary')) {
            row.addClass('first-level-admin');
        } else if ($(this).hasClass('btn-warning')) {
            row.addClass('second-level-admin');
        } else if ($(this).hasClass('btn-danger')) {
            row.addClass('suspended-admin');
        }
    });
});
