$(function () {
    var $comCont = $('#newCommentContainer');
    var $comVal = $('#newComment');
    var $escOptions = $('#escOptions');
    var $script = $('#wf1');
    var TickType = $script.attr('data-TickType');
    var Level = $script.attr('data-level');
    var secToken = $('[name=__RequestVerificationToken]').val();

    $('#addComment').click(function () {
        $comCont.slideToggle(400);
    });

    $('#submitComment').click(function () {
        if ($comVal.val()) {
            $.post('/Workflow/Workflow/_PostNewComment', { commentText: $comVal.val(), __RequestVerificationToken: secToken }, function (result) {
                $comCont.after(result);
                $('#noComments').remove();
                $comCont.next().slideToggle(400);
            });
            $comCont.slideToggle(400);
            $comVal.val('');
        }
    });

    if ($escOptions.length) {
        $.get('/Workflow/Workflow/_GetEscalationOptions', ({ ticketType: TickType, level: Level}), function(result){
            $escOptions.children().replaceWith(result);
            if($escOptions.find('#emptyEsc').length){
                $escOptions.attr('disabled', 'disabled');
            };
        });
    }

    $('#sendConfirm').on('show.bs.modal', function (event) {
        var button = $(event.relatedTarget);
        var action = button.data('action');
        var recipient = button.html();

        var modal = $(this);
        modal.find('#sendTckCon').attr('href', '/Workflow/Workflow/SendTicket?lvlAction=' + action);
        modal.find('#sendTckCon').html('Send to ' + recipient);
        modal.find('#sendTo').html(recipient);
    });
});