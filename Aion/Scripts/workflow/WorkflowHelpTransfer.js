$(function () {
    var $script = $('#wf4');

    $('input[type=date]').attr({
        value: $script.attr('today'),
        disabled: true
    }).parent().toggle();

    $('input:radio').change(function () {
        if (this.value == "No") {
            $('input[type=date]').attr({
                disabled: false
            }).parent().slideDown(350);
        }
        else
        {
            $('input[type=date]').attr({
                disabled: true
            }).parent().slideUp(350);
        }
    });
});