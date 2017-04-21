
//避免重复发送请求
function CheckIsFellow(Obj, UserId) {
    var UserThis = $(Obj);
    $.ajax({
        type: "POST",
        dataType: "text",
        url: $Maticsoft.BasePath + "Profile/AjaxCheckIsFellow",
        data: { UserId: UserId },
        success: function (data) {
            if (data == "True") {
                UserThis.hide().next().show();
            }
        }
    });
}
$(function () {
    $(".FellowUser").each(function () { if ($(this).attr("autotest") != "false") { CheckIsFellow($(this), $(this).attr("UserId")) } });
    $(".FellowUser").die("click").live("click", function (e) {
        if (CheckUserState()) {
            e.preventDefault();
            var UserId = $(this).attr("UserId");
            var UserThis = $(this);
            $.ajax({
                type: "POST",
                dataType: "text",
                url: $Maticsoft.BasePath + "Profile/AjaxFellowUser",
                data: { UserId: UserId },
                async: false,
                success: function (data) {
                    if (data == "False") {
                        $.jBox.tip('亲，失败了....', 'success');
                    }
                    else if (data == "Self") {
                        $.jBox.tip('自己不能关注自己', 'error');
                    }
                    else {
                        UserThis.hide().next().show();
                    }
                }
            });
        }
    });

    $(".UnFellowUser").die("click").live("click", function (e) {
        e.preventDefault();
        if (CheckUserState()) {
            var UserId = $(this).attr("UserId");
            var UserThis = $(this);
            $.ajax({
                type: "POST",
                dataType: "text",
                async: false,
                url: $Maticsoft.BasePath + "Profile/AjaxUnFellowUser",
                data: { UserId: UserId },
                success: function (data) {
                    if (data == "False") {
                        $.jBox.tip('亲，失败了....', 'success');
                    }
                    else {
                        UserThis.hide().prev().show();

                    }
                }
            });
        }
    });
});





