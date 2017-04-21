var GroupId = $("#groupid").val();
var isJoin = false;
var CheckUserIsJoinGroup = function () {
    if (CheckUserState()) {
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AJaxCheckUserIsJoinGroup",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { GroupId: $("#groupid").val() },
            async: false,
            success: function (resultData) {
                if (resultData == "joined") {
                    isJoin = true;
                    $("#joingroup").hide();
                    //$.jBox.tip('您已经加入', 'success');
                }
                else {
                    isJoin = false;
                    $.jBox.confirm("确定加入此小组确定吗？", "提示", submitJoin);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
        return isJoin;
    }
}

var submitJoin = function (v, h, f) {
    if (v == 'ok') {
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxJoinGroup",
            type: 'post', dataType: 'text', timeout: 10000,
            async: false,
            data: { GroupId: $("#groupid").val() },
            success: function (resultData) {
                if (resultData == "joined") {
                    isJoin = true;
                    $("#joingroup").hide();
                    $.jBox.tip('您已经加入', 'success');
                }
                else if (resultData == "Yes") {
                    isJoin = true;
                    $("#joingroup").hide();
                    $.jBox.tip('您已经成功加入', 'success');
                }
                else { isJoin = false; $.jBox.tip('出现异常', 'error'); }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    }
    return true;
};

