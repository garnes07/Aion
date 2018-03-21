$(function () {
    var empNum = 0;
    var first = true;
    var $helpBody = $('#helpBody');
    var contentHtml = $helpBody.html();

    $('.helpModal').click(function () {
        empNum = $(this).data('empnum');
        if (!first) {
            $helpBody.html(contentHtml);
        }
        else {
            first = !first;
        };
        $('#helpModal').modal('toggle');
    });

    $('#helpModal').on('click', '.tckRaise', function () {
        var form = $('<form></form>');
        console.log($(this));

        form.attr('method', 'post');
        form.attr('action', '/WFM/RFTP/RaiseTicket');
        form.attr('target', '_blank');

        form.append('<input type="hidden" name="empId" value="' + empNum + '">');
        form.append('<input type="hidden" name="formType" value="' + $(this).data('formtype').toString() + '">');

        $(document.body).append(form);
        form.submit();

        $('#helpModal').modal('toggle');
    });

    //base to level 1
    $('#helpModal').on('click', '.lvl1', function () {
        var path = $(this).data('step').toString();
        $('#baseContainer').slideUp(350, function () {
            $('#' + path).slideDown(350);
        });
    });

    //level 1 to level 2
    $('#helpModal').on('click', '.lvl2', function () {
        var path = $(this).data('step').toString();
        $('#level1').slideUp(350, function () {
            $('#' + path).slideDown(350);
        });
    });

    //level 2 to 3
    $('#helpModal').on('click', '.lvl3', function () {
        var path = $(this).data('step').toString();
        $('#level2').slideUp(350, function () {
            $('#' + path).slideDown(350);
        });
    });

    //change path 3
    $('#helpModal').on('click', '.lvlup3', function () {
        var path = $(this).data('step').toString();
        $('#3_1').slideUp(350, function () {
            $('#' + path).toggle()
            $('#3').toggle();
            $('#level1').slideDown(350);
        });
    });

    //change path 4
    $('#helpModal').on('click', '.lvlup4', function () {
        var path = $(this).data('step').toString();
        $('#5_2').slideUp(350, function () {
            $('#' + path).slideDown(350);
        });
    });
});