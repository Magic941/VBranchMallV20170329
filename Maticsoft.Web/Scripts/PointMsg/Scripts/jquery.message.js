//by minamiko
jQuery.MessageShow = function (msg) {
    $("<div id='message'>" + msg + "</div>").appendTo("body").hide().css("top", ($(window).height()+ $(window).scrollTop() - 63) / 2).css("left", ($(document).width() - 372) / 2).fadeIn("fast").delay(2000).fadeOut("slow", function () {
        $(this).remove();
    });
}