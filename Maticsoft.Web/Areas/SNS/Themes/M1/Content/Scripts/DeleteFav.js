$(function () {
    $(".delfav").die("click").live("click", function (e) {
        e.preventDefault();
        if (CheckUserState()) {
            var Type = $(this).attr("imagetype");
            var TargetID = $(this).attr("targetid");
            var NowThis = $(this);
            $.ajax({
                type: "POST",
                dataType: "text",
                url: $Maticsoft.BasePath + "Profile/AjaxDelFav",
                data: { Type: Type, TargetId: TargetID },
                success: function (data) {
                    if (data == "False") {
                        $.jBox.tip('删除失败', 'success');
                    }
                    else {
                        NowThis.parents(".i_w_y").hide(0);
                    }
                }
            });
        }
    })

})