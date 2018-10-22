$(function () {
    var $script = $('#wfCR');
    var branch = $script.attr('data-branch');
    var exception = $script.attr('data-exception');

    if (exception == 1) {
        $('#tckHeader').after('<div class="alert alert-danger text-center"><strong>As part of completing this ticket the timecard must also be signed off</strong></div>')
    };  

    if (branch != null) {
        var $toChange = $('#q_2');
        var current = $toChange.html();
        $toChange.html(current + " - ...");

        $.get('/Workflow/Workflow/_getRegionTemp', { storeNum : branch}, function (result) {
            $toChange.html(current + " - Region " + result);
        });
    };
});