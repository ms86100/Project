$(function () {

    ShowMessageBox();
    ShowMailMessageBox();

    //**************** JS to show loading progress during ajax call *********************//
    $(document).ajaxStart(function () {
        $("#ajaxLoading").css("display", "block");
        $("#ajaxLoading").css("top", $(window).height() / 1.8);
        $("#ajaxLoading").css("left", $(window).width() / 2);
        $("#ajaxLoading").css("position", "fixed");
    });
    $(document).ajaxComplete(function () {
        $("#ajaxLoading").css("display", "none");
    });


    //******************************** JS for Grid paging *********************************//
    var getPage = function () {
        var $a = $(this);

        if ($a.attr("href").trim() == undefined || $a.attr("href").trim() == "") {
            return;
        }
        var options = {
            url: $a.attr("href")
            , data: $("form").serialize()
            , type: "get"
        }

        $.ajax(options).done(function (data) {
            var $target = $($a.parents("div.ns-grid-pager").attr("data-otf-target"));
            $target.replaceWith(data);
        });
        return false;
    };
    $("#content").on("click", "a.ns-page-link", getPage);

    var getPageForDDL = function () {
        var TargetURL = $(this).parent().attr("data-otf-actionlink");
        TargetURL = TargetURL + "?PageSize=" + $('.page-size').val() + "&PageNo=" + $(this).val()
        var options = {
            url: TargetURL
            , data: $("form").serialize()
            , type: "get"
        }
        var target = $(this).parent().attr("data-otf-target");
        $.ajax(options).done(function (data) {
            $(target).replaceWith(data);
        });
    };
    $("#content").on("change", ".page-number", getPageForDDL);

    var getPageSizeForDDL = function () {
        var TargetURL = $(this).parent().attr("data-otf-actionlink");
        TargetURL = TargetURL + "?PageSize=" + $(this).val()
        var options = {
            url: TargetURL
            , data: $("form").serialize()
            , type: "get"
        }
        var target = $(this).parent().attr("data-otf-target");
        $.ajax(options).done(function (data) {
            $(target).replaceWith(data);
        });
    };
    $("#content").on("change", ".page-size", getPageSizeForDDL);

    $("#content").on("click", ".resend", ShowResendMessageBox);

    $("#content").on("click", ".delete", ShowWarningMessageBox);

    $('#search').on("click", function () {
        $('#allList').show();
    });

    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action")
            , type: $form.attr("method")
            , data: $form.serialize()
        }

        var target = $($form.attr("data-otf-target"));
        $.ajax(options).done(function (data) {
            $(target).replaceWith(data);
        });
        return false;
    };
    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);

    var url = window.location.href;
    var pathArray = location.pathname.split('/');
    var path = pathArray[1];
    // passes on every "a" tag
    $("#dock a").each(function () {
        // checks if its the same on the address bar
        var href = $(this).attr('href');
        if (href.indexOf(path) >= 0)
        {
            $(this).parents("li").addClass("active");
        }
        //if (url == (this.href)) {
        //    $(this).parents("li").addClass("active");
        //}
    });
    
});

function CustomAjaxFormSubmit(sender, url) {
    if (url == "#")
    { return false; }
    var $form = $('a[href="' + decodeURI(url) + '"]').closest('form')
    if ($form.attr("data-otf-ajax") == 'true') {
        var options = {
            url: decodeURI(url)
            , type: $form.attr("method")
            , data: $form.serialize()
        }
        var target = $($form.attr("data-otf-target"));
        $.ajax(options).done(function (data) {
            $(target).replaceWith(data);
            //location.reload();
            ShowMessageBox();
            ShowMailMessageBox();
        });
        return false;
    }
    else {
        return true;
    }
};

function ShowMessageBox() {
    var msgId = $('#ErrMsgHiddenField').val();
    if (msgId != null && msgId.toString().trim() != "") {
        if (msgId == "0") {
            $('#myModalLabel').html('<b class="text-danger">Error</b>');
            $('#SomeMsg').text('Operation fail...');
        }
        else if (msgId == "1") {
            $('#myModalLabel').html('<b class="text-success">Success</b>');
            $('#SomeMsg').text("Operation success...");
        }
        else if (msgId == "2") {
            $('#myModalLabel').html('<b class="text-info">Information</b>');
            $('#SomeMsg').text('Operation failed. Unique key constrined is found..');
        }
        else if (msgId == "3") {
            $('#myModalLabel').html('<b class="text-info">Information</b>');
            $('#SomeMsg').text('Operation failed. Please enter activities in a daily sequential manner.');
        }
        else if (msgId == "4") {
            $('#myModalLabel').html('<b class="text-danger">Error</b>');
            $('#SomeMsg').text('You cannot delete your own account.');
        }
        else if (msgId == "5") {
            $('#myModalLabel').html('<b class="text-info">Information</b>');
            $('#SomeMsg').text('Please enter daily activites for all days of Tour Package');
        }
        else if (msgId == "6") {
            $('#myModalLabel').html('<b class="text-info">Information</b>');
            $('#SomeMsg').text('Please select a default picture for your Tour Package');
        }
        else if (msgId == "7") {
            $('#myModalLabel').html('<b class="text-success">Success</b>');
            $('#SomeMsg').text('Your Tour Package has been successfully created.');
        }
        else if (msgId == "8") {
            $('#myModalLabel').html('<b class="text-info">Information</b>');
            $('#SomeMsg').text('Tour package details are not complete. Please edit your Tour Package');
        }
        $('#myErroMsgModalYesButton').addClass('hidden');
        $('#myErroMsgModal').show();
    }
}

function ShowMailMessageBox() {
    var msg = $('#MailMsgHiddenField').val();
    if(msg!=null && msg.toString().trim()!="")
    {
        if (msg == "1") {
            $('#myMailModalLabel').html('<b class="text-success">Success</b>');
            $('#customMsg').text("Confirmation Mail has been sent to User's email address");
        }
        else if (msg == "0") {
            $('#myMailModalLabel').text('<b class="text-danger">Error</b>');
            $('#customMsg').text("Sorry, Mail could not be sent at this time. Try Again later.");
        }
        else if (msg == "2") {
            $('#myMailModalLabel').html('<b class="text-info">Information</b>');
            $('#customMsg').text("User has been created but mail could not be sent at this time.");
        }
        $('#myMailMsgModal').modal('show');
    }
}


var ShowWarningMessageBox = function (e) {
    if ($(this).text() != "Cancel") {
        // Set the sender infromation in hidden field and its closest form
        $("#eventSender").val(($(this).attr('href')) + '|' + $($(this).closest('form')));

        $('#myModalLabel').html('<b class="text-info">Information</b>');

        $('#SomeMsg').text('Are you sure that you want to delete the record..?');

        $('#myErroMsgModalYesButton').removeClass('hidden');
        $('#myErroMsgModal').show();
        e.preventDefault();
    }
}

var ShowResendMessageBox = function (e) {
    if ($(this).text() != "Cancel") {
        // Set the sender infromation in hidden field and its closest form
        $("#eventSender").val(($(this).attr('href')) + '|' + $($(this).closest('form')));

        $('#myModalLabel').html('<b class="text-info">Information</b>');

        $('#SomeMsg').text('Do you want to resend confirmation email..?');

        $('#myErroMsgModalYesButton').removeClass('hidden');
        $('#myErroMsgModal').show();
        e.preventDefault();
    }
}
// Close message box
function CloseMyModal() {
    $('#myErroMsgModalYesButton').addClass('hidden');
    $("#myErroMsgModal").hide();
}
// close message box and procceed for intended action.
function OkMyModal() {
    $("#myErroMsgModal").hide();
    // Retrieve the sender infromation from hidden field and pass it to the function
    CustomAjaxFormSubmit($("#eventSender").val().split('|')[1], $("#eventSender").val().split('|')[0]);
}

