$(function () {
    ShowMessageBox();

    //**************** JS to show loading progress during ajax call *********************//
    $(document).ajaxStart(function () {
        $("#ajaxLoading").css("display", "block");
        $("#ajaxLoading").css("top", $(window).height() / 2);
        $("#ajaxLoading").css("left", $(window).width() / 2);
        $("#ajaxLoading").css("position", "fixed");
    });
    $(document).ajaxComplete(function () {
        $("#ajaxLoading").css("display", "none");
    });

    //jquery for read more/read less toggle
    var showChar = 300;
    var ellipsestext = "...";
    var moretext = "Show more";
    var lesstext = "Show less";
    $('.more').each(function () {
        var content = $(this).html();

        if (content.length > showChar) {

            var c = content.substr(0, showChar);
            var h = content.substr(showChar, content.length - showChar);
            var html = c + '<span class="moreelipses">' + ellipsestext + '</span><span class="morecontent"><span>' + h + '</span>&nbsp;&nbsp;<a href="" class="morelink">' + moretext + '</a></span>';
            $(this).html(html);
        }

    });

    $(".morelink").click(function () {
        if ($(this).hasClass("less")) {
            $(this).removeClass("less");
            $(this).html(moretext);
        } else {
            $(this).addClass("less");
            $(this).html(lesstext);
        }
        $(this).parent().prev().toggle();
        $(this).prev().toggle();
        return false;
    });

    var url = window.location.href;

    // passes on every "a" tag
    $("#navigation a").each(function () {
        // checks if its the same on the address bar
        if (url == (this.href)) {
            $(this).addClass("activea");
            $(this).parents("li").addClass("activa");
        }
    });

    var str = location.href.toLowerCase();
    $('#main-nav1 a').each(function () {
        if (str == this.href) {
            $("li.highlight").removeClass("highlight");
            $(this).parents().addClass("highlight");
        }
    });

    var ajaxFormSubmit = function () {
        var $form = $(this);
        var options = {
            url: $form.attr("action")
            , type: $form.attr("method")
            , data: $form.serialize()
        }
        $.ajax(options);
    };
    $("form[data-otf-ajax='true']").submit(ajaxFormSubmit);

});
function ShowMessageBox() {
    var msgId = $('#ErrMsgHiddenField11').val();
    if (msgId != null && msgId.toString().trim() != "") {
        if (msgId == "0") {
            $('#myLabel1').html('<b class="text-danger">Error</b>');
            $('#Msg').text('Your request cannot be completed at this time. Please Try Again Later.');
        }
        else if (msgId == "1") {
            $('#myLabel1').html('<b class="text-success">Success</b>');
            $('#Msg').text("Your request has been successfully submitted. We will be in touch soon.");
        }
        else if (msgId == "2") {
            $('#myLabel1').html('<b class="text-info">Information</b>');
            $('#Msg').text('Operation failed. Unique key constrined is found..');
        }
        else if (msgId == "3") {
            $('#myLabel1').html('<b class="text-info">Information</b>');
            $('#Msg').text('Operation failed. Refferencial key constrined is found..');
        }
        $('#myErroMsgModal').show();
    }
}
function CloseMyModal() {
    $('#myErroMsgModalYesButton').addClass('hidden');
    $("#myErroMsgModal").hide();
}
