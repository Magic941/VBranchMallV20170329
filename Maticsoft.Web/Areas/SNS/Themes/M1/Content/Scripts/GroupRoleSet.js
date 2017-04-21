$(function () {
    $(".setAdmin").click(function () {
        var groupid = $(this).attr("data-gid");
        var userid = $(this).attr("data-uid");
        var ObjectThis = $(this)
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Group/AjaxUserRoleUpdate",
            async: false,
            data: { GroupId: groupid, UserId: userid, Role: 1 },
            success: function (resultData) {
                if (resultData == "Success") {
                    ObjectThis.hide().prev().show();
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("操作失败");
            }
        });


    });
    $(".cancAdmin").click(function () {
        var groupid = $(this).attr("data-gid");
        var userid = $(this).attr("data-uid");
        var ObjectThis = $(this);
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Group/AjaxUserRoleUpdate",
            async: false,
            data: { GroupId: groupid, UserId: userid, Role: 0 },
            success: function (resultData) {
                if (resultData == "Success") {
                    ObjectThis.hide().next().show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("操作失败");
            }
        });


    });

})