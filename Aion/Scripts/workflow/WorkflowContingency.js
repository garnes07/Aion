$(function () {
    var a = false;
    var b = false;

    $('#crReminder').modal('show');

    $('#chkA').click(function () {
        a = !a;
        if (a && b) {
            $('#crDismiss').prop('disabled', false);
        }
        else {
            $('#crDismiss').prop('disabled', true);
        }
    });

    $('#chkB').click(function () {
        b = !b;
        if (a && b) {
            $('#crDismiss').prop('disabled', false);
        }
        else {
            $('#crDismiss').prop('disabled', true);
        }
    });
});