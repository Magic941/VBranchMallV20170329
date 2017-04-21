$(document).ready(function () {
    $(".leixing").find("a").each(function () {
        $(this).click(function () {
            $(this).parent().find("a").each(function () {
                var h = $(this).find("font").html();
                $(this).removeClass("lx_sel");
                $(this).html(h);
            });
            if ($(this).find("span").length == 0) {
                $(this).html("<font>" + $(this).html() + "</font><span onclick='cancelChoose(this)'></span>");
                $(this).addClass("lx_sel");
            }
        });
    });
    
});

function cancelChoose(cancelObj) {
    event.stopPropagation();
    var content = $(cancelObj).siblings("font").html();
    $(cancelObj).parents("a").removeClass("lx_sel").html(content);
}