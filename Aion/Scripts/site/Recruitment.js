//Prevent duplicate submission on double click
jQuery.fn.PreventDoubleSubmission = function () {
    var $form = $(this);
    if ($form.data('submitted') !== true) {
        $form.data('submitted', true);
        $form.submit();
    };
    return this;
};

//Allowance detail object
function AllowanceDetail(array) {
    this.RoleId = parseInt(array[0]),
    this.RoleAllowance = parseInt(array[1]),
    this.ExistingVacancyHours = parseInt(array[2]),
    this.FriendlyName = array[3],
    this.HourlyRate = parseFloat(array[4]),
    this.ContractBase = parseFloat(array[5]),
    this.ChecksVisible = false,
    this.Action = 'new',
    this.Repost = 'true';
};
// [RoleId, RoleAllowance, ExistingVacancy, FriendlyName, HourlyRate]
var roleAllowance = [
    //new AllowanceDetail(1, 45, 30, 'Sales Consultant', 1),
    //new AllowanceDetail(2, 25, 25, 'Team KnowHow Colleague', 1),
    //new AllowanceDetail(3, 10, 0, 'Team KnowHow Engineer', 1)
];
roleAllowance.findByRoleId = function (_RoleId) {
    for (var i = 0; i < this.length; i++) {
        if (this[i].RoleId == _RoleId) {
            return this[i];
        };
    };
    return -1;
}
roleAllowance.findAllowance = function (item) {
    var _entry = this.findByRoleId(item);
    if (_entry != -1) {
        if (_entry.Action === 'replace' && _entry.RoleAllowance != -1 && _entry.ContractBase != 0)
            return _entry.RoleAllowance + _entry.ExistingVacancyHours;
        else
            return _entry.RoleAllowance;
    }
};
roleAllowance.existingVacancyIds = function (array) {
    var toReturn = [];
    for (var i = 0; i < this.length; i++) {
        if (array.indexOf(this[i].RoleId) != -1 && this[i].ExistingVacancyHours >= 1) {
            toReturn.push(this[i].RoleId);
        };
    };
    return toReturn;
};
roleAllowance.VacancyToReplace = function () {
    var toReturn = 0;
    for (var i = 0; i < this.length; i++) {
        if (this[i].Action === 'replace') {
            toReturn += this[i].ExistingVacancyHours;
        };
    };
    return toReturn;
};

var $newRow;
var $templateRow
var $templateCheckRow;
var grpCount = 1;        
var rowErrors = [];
var rowWarnings = [];
var $form = $('#rRequest');
var $submit = $('#btnSubmit');
var $notes = $('#notes');
var $pContainer = $('#pContainer');
var $baseAlert = $('#baseAlert');
var $baseAlertNum = $('#baseAlertNum');
var allowSubmit = false;
var checksValid = true;
var hoursBuffer = $('#scrpt').data('buffer');
var totalBase = parseFloat($('#scrpt').data('totalbase'));
var currentBase = parseFloat($('#scrpt').data('currentbase'));
var overBase = false;
var warningBase = false;
var roleList;

//On Start
$(function () {
    $templateRow = $('#r_0').clone();
    $templateRow.find('.add').after('<i class="col text-danger icon ion-minus-circled remove" style="font-size:larger; cursor:pointer;"></i>');
    $templateCheckRow = $('#p_0').clone();
    $pContainer.html('');

    roleList = $('#scrpt').data('roledetail');
    //roleList = JSON.parse($('#testInput').html());

    for (var i = 0; i < roleList.length; i++) {
        roleAllowance.push(new AllowanceDetail(roleList[i]));
    };
});

//Find total hours in form
function findTotal(a) {
    var total = 0;
    $form.find('.request').each(function (x) {
        if ($(this).find('.position').val() == a) {
            total += parseInt($(this).find('.rowTotal').val());
        }
    });
    return total;
};

function TotalsCheck(position) {
    if (totalBase == -1) {
        checkTotals(position);
    }
        //else if (overBase || warningBase) {
        //    if (warningBase) {
        //        approvalResult = 'ion-alert text-warning';
        //        approvalVal = "review";
        //    } else {
        //        approvalResult = 'ion-checkmark-round text-success';
        //        approvalVal = "approved";
        //    };

        //    $form.find('.request').each(function (x) {
        //        $(this).find('.approvalResult').attr('class', 'approvalResult icon ' + approvalResult);
        //        $(this).find('.approval').val(approvalVal);
        //    });
        //}
    else {
        var positionList = [];
        $form.find('.position').each(function () {
            positionList.push(parseInt($(this).val()));
        });
        for (var i = 0; i < positionList.length; i++) {
            checkTotals(positionList[i]);
        };
    };
};

//Check totals and apply validation by roleId
function checkTotals(position) {
    var allowance = roleAllowance.findAllowance(position);
    var positionDetail = roleAllowance.findByRoleId(position);
    var positionTotal = findTotal(position);
    var currentBase = positionDetail.ContractBase - allowance;

    var approvalResult;
    var approvalVal;
    if (allowance == -1 && positionDetail.ContractBase == 0) {
        if (warningBase) {
            approvalResult = 'ion-alert text-warning';
            approvalVal = "review";
        } else {
            approvalResult = 'ion-checkmark-round text-success';
            approvalVal = "approved";
        };
    }
    else {
        if (positionTotal + currentBase > positionDetail.ContractBase * hoursBuffer || overBase) {
            approvalResult = 'ion-close-round text-danger';
            approvalVal = "reject";
            if (rowErrors.indexOf(position) == -1) {
                rowErrors.push(position);
            };
            allowSubmit = false;
            $submit.addClass('disabled').attr('disabled', true);
            $notes.attr('required', false);
            $('#notesError').addClass('d-none');
            $('#notes').removeClass('border-danger');
        }
        else if (positionTotal > allowance || warningBase) {
            approvalResult = 'ion-alert text-warning';
            approvalVal = "review";
            if (rowErrors.indexOf(position) != -1) {
                rowErrors.splice(rowErrors.indexOf(position), 1);
            };
            if (rowErrors.length === 0 && !overBase) {
                allowSubmit = true;
                $submit.removeClass('disabled').attr('disabled', false);
                $notes.attr('required', true);
            };
            if (rowWarnings.indexOf(position) == -1) {
                rowWarnings.push(position);
            };
        }
        else {
            approvalResult = 'ion-checkmark-round text-success';
            approvalVal = "approved";
            if (rowErrors.indexOf(position) != -1) {
                rowErrors.splice(rowErrors.indexOf(position), 1);
            };
            if (rowWarnings.indexOf(position) != -1) {
                rowWarnings.splice(rowWarnings.indexOf(position), 1);
            };
            if (rowErrors.length === 0 && !overBase) {
                allowSubmit = true;
                $submit.removeClass('disabled').attr('disabled', false);
            };
            if (rowWarnings.length === 0) {
                $notes.attr('required', false);
                $('#notesError').addClass('d-none');
                $('#notes').removeClass('border-danger');
            };
        };
    };

    $form.find('.request').each(function (x) {
        if ($(this).find('.position').val() === position) {
            $(this).find('.approvalResult').attr('class', 'approvalResult icon ' + approvalResult);
            $(this).find('.approval').val(approvalVal);
        };
    });
};


//Display checks for request with existing vacancies
function AddChecks(existingPositions) {
    for (var i = 0; i < roleAllowance.length; i++) {
        if (existingPositions.indexOf(roleAllowance[i].RoleId) != -1) {
            if (!roleAllowance[i].ChecksVisible) {
                var $newCheckRow = $templateCheckRow.clone();
                $newCheckRow.attr('id', $newCheckRow.attr('id').replace('0', roleAllowance[i].RoleId));
                $newCheckRow.find('.position').html(roleAllowance[i].FriendlyName);
                $newCheckRow.find(':input').each(function () {
                    $(this).attr('name', $(this).attr('name').replace('0', roleAllowance[i].RoleId)).attr('id', $(this).attr('id').replace('0', roleAllowance[i].RoleId));
                });
                $newCheckRow.find('label').each(function () {
                    $(this).attr('for', $(this).attr('for').replace('0', roleAllowance[i].RoleId));
                });
                $pContainer.append($newCheckRow);
                roleAllowance[i].ChecksVisible = true;
                $submit.addClass('disabled').attr('disabled', true);
                checksValid = false;
            }            
        }
        else {
            $pContainer.find('#p_' + roleAllowance[i].RoleId).remove();
            roleAllowance[i].ChecksVisible = false;
            roleAllowance[i].Action = 'new';
            roleAllowance[i].Repost = 'true';
        };
    };
    
    $('#checkContainer').removeClass('d-none');

    $('html, body').animate({
        scrollTop: $('#formCard').offset().top
    }, 1000);            
};

function RemoveAllChecks() {
    for (var i = 0; i < roleAllowance.length; i++) {
        roleAllowance[i].ChecksVisible = false;
        roleAllowance[i].Action = 'new';
        roleAllowance[i].Repost = 'true';
    };

    $('#checkContainer').addClass('d-none');
}

//On submit validate criteria
$submit.click(function (event) {
    var totalHours = 0;
    $form.find('.rowTotal').each(function () {
        totalHours += parseInt($(this).val());
    });

    if ($('#notes').attr('required') && $('#notes').val() === '' && $('#notes').val().length < 10) {
        $('#notesError').removeClass('d-none');
        $('#notes').addClass('border-danger');
    }
    else {
        $('#notesError').addClass('d-none');
        $('#notes').removeClass('border-danger');
        if (allowSubmit && totalHours != 0) {
            $form.PreventDoubleSubmission();
        };
    };    
});

//Validate row on change
$form.on('change', ':input', function (e) {
    var $triggerRow = $('#' + e.target.parentElement.parentElement.id);
    var total = $triggerRow.find('.heads').val() * $triggerRow.find('.hours').val();
    if (total > 0) {
        $triggerRow.find('.rowTotal').val(total);
        var position = $triggerRow.find('.position').val();
        var roleDetails = roleAllowance.findByRoleId(position);

        var positionList = [];
        $form.find('.position').each(function () {
            positionList.push(parseInt($(this).val()));
        });
        var existingVacancies = roleAllowance.existingVacancyIds(positionList);
        
        if (e.target.classList.contains('position') || !roleDetails.ChecksVisible) {
            if (e.target.classList.contains('position')) {
                $triggerRow.find('.Action').val(roleDetails.Action);
                $triggerRow.find('.Repost').val(roleDetails.Action);
                $triggerRow.find('.approvalResult').attr('class', 'approvalResult icon');
                for (var i = 0; i < positionList.length; i++) {
                    if (rowErrors.indexOf(positionList[i]) == -1) {
                        rowErrors.splice(rowErrors.indexOf(positionList[i]), 1);
                    };
                    if (rowWarnings.indexOf(positionList[i]) == -1) {
                        rowWarnings.splice(rowWarnings.indexOf(positionList[i]), 1);
                    };
                };
            };

            if (existingVacancies.length) {
                AddChecks(existingVacancies);
            }
            else {
                RemoveAllChecks();
            };
        };

        if (checksValid) {
            if (totalBase != -1) {
                CheckAgainstBase();
            };
            TotalsCheck(position);
            if (overBase) {
                $('.approvalResult').addClass('d-none');
            }
            else {
                $('.approvalResult').removeClass('d-none');
            };
        };
    }
    else {
        $triggerRow.find('.rowTotal').val(0);
        $triggerRow.find('.approvalResult').attr('class', 'approvalResult icon');
    }
});

//Validate checks on change and apply logic to request rows if true
$pContainer.on('change', ':input', function (e) {
    var $triggerRow = $('#' + e.target.parentElement.parentElement.id);
    $triggerRow.find('.icon').attr('class', 'icon');
    checksValid = ValidateChecks();
    if (checksValid) {
        $form.find('.request').each(function () {
            var position = $(this).find('.position').val();
            if (totalBase != -1) {
                CheckAgainstBase();
            };            
            TotalsCheck(position);
        });
    };
});

//Validate check fields are completed, apply logic to each request if true
function ValidateChecks() {
    var allSelected = true;
    $pContainer.find('.pRow').each(function () {
        if ($(this).find(':input[type=radio]:checked').length == 0)
            allSelected = false;
    });
    if (!allSelected) {
        $('#pError').removeClass('d-none');
        return false;
    }
    else {
        $pContainer.find('.pRow').each(function () {
            var position = $(this).attr('id');
            position = position.replace('p_', '');
            var actionValue = $(this).find(':input[type=radio]:checked').val();
            var repostValue = $(this).find(':input[type=checkbox]').is(':checked');

            roleAllowance.findByRoleId(position).Action = actionValue;
            roleAllowance.findByRoleId(position).Repost = repostValue;
            $form.find('.request').each(function () {
                if ($(this).find('.position').val() == position) {
                    $(this).find('.action').val(actionValue);
                    $(this).find('.repost').val(repostValue);
                };
            });
        });
        return true;
    };
};

//Check roles against max contract base
function CheckAgainstBase() {
    var positionList = [];
    $form.find('.position').each(function () {
        positionList.push(parseInt($(this).val()));
    });
    var total = currentBase;
    var done = [];
    for (var i = 0; i < positionList.length; i++) {
        if (done.indexOf(positionList[i]) == -1) {
            total += findTotal(positionList[i]) * roleAllowance.findByRoleId(positionList[i]).HourlyRate;
            done.push(positionList[i]);
        };
    };
    total -= roleAllowance.VacancyToReplace();
    if (total < totalBase * 1.02) {
        $baseAlert.attr('class', 'alert alert-danger text-center d-none');
        allowSubmit = true;
        $submit.removeClass('disabled').attr('disabled', false);
        $('.approvalResult').removeClass('d-none');
        overBase = false;
        warningBase = false;
        $notes.attr('required', false);
    }
    else if (total < totalBase * hoursBuffer) {
        $baseAlert.attr('class', 'alert alert-warning text-center d-none');
        allowSubmit = true;
        $submit.removeClass('disabled').attr('disabled', false);
        $('#baseWarning').removeClass('d-none');
        $('.approvalResult').removeClass('d-none');
        $notes.attr('required', true);
        warningBase = true;
        overBase = false;
    }
    else {
        allowSubmit = false;
        $submit.addClass('disabled').attr('disabled', true);
        $baseAlertNum.html(parseFloat(total - totalBase).toFixed(2));
        $('#baseWarning').addClass('d-none');
        $baseAlert.attr('class', 'alert alert-danger text-center');
        $notes.attr('required', false);
        overBase = true;
        warningBase = false;
    };
};

//Add new request row
$form.on('click', '.add', function (e) {
    var $triggerRow = $('#' + e.target.parentElement.parentElement.parentElement.id);
    var $newRow = $templateRow.clone();
    $newRow.attr('id', $newRow.attr('id').replace('0', grpCount));
    $newRow.find(':input').each(function () {
        if (!$(this).is('[readonly]')) {
            var val = $(this).attr('name');
            $(this).attr('name', val.replace('0', grpCount));
        };
        if ($(this).attr('name') === 'RowNum') {
            $(this).val(grpCount);
        }        
    });

    var roleCheck = roleAllowance.findByRoleId($newRow.find('.position').val())
    if (roleCheck.ChecksVisible) {
        $newRow.find('.action').val(roleCheck.Action);
        $newRow.find('.repost').val(roleCheck.Repost);
    };
    $triggerRow.after($newRow);
    grpCount++;
});

//Remove specified request row
$form.on('click', '.remove', function (e) {
    var $row = $('#' + e.target.parentElement.parentElement.parentElement.id);
    $row.remove();

    var positionDetail = roleAllowance.findByRoleId($row.find('.position').val());
    var positionTotal = findTotal(positionDetail.RoleId);

    if (positionTotal == 0) {
        positionDetail.Repost = 'true';
        positionDetail.Action = 'new';
        positionDetail.ChecksVisible = false;
        $('#p_' + positionDetail.RoleId).remove();
    };
    if (totalBase != -1) {
        CheckAgainstBase();
    };
    TotalsCheck(positionDetail.RoleId);
});