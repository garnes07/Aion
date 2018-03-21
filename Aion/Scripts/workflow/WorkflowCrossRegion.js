$(function () {
    var $form = $('#formContainer');
    var $exception = $('#exception');
    var $script = $('#wf1');
    var confirmed = false;

    $('#crReminder').modal('show');

    Date.prototype.getWeek = function() {
        var onejan = new Date(this.getFullYear(), 0, 1);
        return Math.ceil((((this - onejan) / 86400000) + onejan.getDay() + 1) / 7);
    };

    Date.prototype.addDays = function (days) {
        var dat = new Date(this.valueOf());
        dat.setDate(dat.getDate() + days);
        return dat;
    };

    $('#confException').click(function () {
        confirmed = true;
        $exception.val(1);
        $form.submit();
    });

    $('#cancException').click(function () {
        confirmed = false;
        $exception.val(0);
        $('#crException').modal('toggle');
    })

    $('input[type=date]').attr({
        value: $script.attr('today'),
        min: $script.attr('min')
    });

    $form.submit(function (event) {
        var valid = true;
        var exception = false;

        $(this).find('[id*="grp"]').each(function () {
            var start = $(this).find('select').eq(0).val();
            var end = $(this).find('select').eq(1).val();

            var dateString = $(this).find('input').eq(3).val();
            var parts = dateString.split('-');
            var myDate = new Date(parts[0], parts[1] - 1, parts[2]);
            var today = new Date();
            today.setHours(0, 0, 0, 0);

            if ((today.getDay() <= 1 && myDate < today.addDays(today.getDay())) || (today.getDay() == 5 && myDate.getHours() >= 15) || (today.getDay() == 6 && myDate < today.addDays(7 - today.getDay()))) {
                exception = true;
            }

            if (start > end) {
                if (!$(this).find('.alert').length) {
                    $(this).append('<div class="alert alert-danger text-center"><strong>Start time must be earlier than end time!</strong></div>');
                };
                valid = false;
            }
            else {
                $('.alert', this).remove();
            };
        });
        //event.preventDefault();

        if ($('#_branchValidate').data('valid') === 0) {
            valid = false;
            $('#_branchValidate').addClass('alert alert-danger');
        };
        
        if (!valid || (exception && !confirmed)) {
            event.preventDefault();
        };
        if (valid && exception && !confirmed) {
            event.preventDefault();
            $('#crException').modal('toggle');
        };        
    });
});