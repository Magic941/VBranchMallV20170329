
$(function () {
    $(".UserTip").each(function () {
        var target = $Maticsoft.BasePath + "Partial/AjaxUserInfo?UserId=" + $(this).attr("UserId") + "";
        if ($(this).attr("NickName")) {
            target+="&NickName=" + $(this).attr("NickName") + "";
        }
        $(this).powerFloat({
            eventType: "hover",
            target: target,
            targetMode: "ajax",
            hoverHold: true,
            showDelay: 0,
            hideDelay: 500
        });

    });
});