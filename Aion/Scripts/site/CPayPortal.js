$(function () {
    var $menu = $('#PayPeriod');
    var $cont = $('#container');
    var $loading = $('#loading');
    var $retry = $('#retry');
    var $error = $('#errorContainer');

    var dataPull = function () {
        if ($menu.val() !== '') {
            $cont.slideUp(300, function () {
                $loading.toggle();
            });
            $error.hide();
            $.get('/WFM/RFTP/_PayData', { period: $menu.val() }, function (result) {
                setTimeout(function () {
                    $cont.html(result);
                    $loading.toggle();
                    $cont.slideDown(300);
                }, 250);
            }).fail(function () {
                $loading.toggle();
                $error.show();
            });
        }
    };

    $menu.change(dataPull);
    $error.click(dataPull);

    $cont.popover({
        selector: '[data-toggle="popover"]',
        trigger: 'focus'
    });

});