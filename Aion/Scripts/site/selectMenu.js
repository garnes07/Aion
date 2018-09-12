$(function () {
    var loaded = false;
    var $tree = $('#tree');
    var crit = $('#mSearch').val();

    $('#h-side-open').click(function () {
        if (!loaded) {
            loadTree();
        };
        $('#h-side').addClass('show');
        $('.overlay').fadeIn();
    });
    
    $('#input-search').keypress(function (e) {
        if (e.which == 13) {
            $('#btn-search').trigger('click');
        }
    });

    $('#btn-search').click(function () {
        $tree.treeview('collapseAll', { silent: true });
        var crit = $('#input-search').val();
        $tree.treeview('search', [crit, {
            ignoreCase: true,
            exactMatch: false,
            revealResults: true
        }]);
    });

    $('#h-side-close, .overlay').click(function () {
        $('#h-side').removeClass('show');
        $('.overlay').fadeOut();
    });

    $('#hSearchbtn').click(function () {
        $('#hSearch').slideToggle();
    });

    $('.clickthrough').click(function (e) {
        var href = this.href;
        var result = undefined;
        var crit = $(this).data('selector');

        event.preventDefault();
        if (loaded) {
            result = clickThrough(crit);
            if (result) {
                window.location = href;
            };
        }
        else {
            loadTree(function () {
                result = clickThrough(crit);
                if (result) {
                    window.location = href;
                };
            });
        };
    });
    
    function clickThrough(s) {
        var result = $tree.treeview('search',
        [String(s), {
            ignoreCase: true,
            exactMatch: true,
            revealResults: false
        },
        'storeNum']);
        return result.length;
    };

    function loadTree(callback) {
        $.get('/Profile/GetMenu', function (result) {
            $tree.treeview({
                data: result,
                levels: 1,
                enableLinks: true,
                defaultSelection: crit
            });
            loaded = true;
            callback();
        });
    };
});
