$(function () {
    $("#cancalRecommand").click(function () {
        var targetid = $(this).attr("targetid");
        var ObjectThis = $(this);
        var TargetType = $(this).attr("imagetype");
        $.ajax({
            url: $Maticsoft.BasePath + "Admin/AjaxRecommandOperation",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { Type: "cancal", TargetId: targetid, TargetType: TargetType },
            success: function (resultData) {
                if (resultData == "Yes") {
                    ObjectThis.hide().next().show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    }).mouseover(function () { $(this).text("取消推荐") }).mouseout(function () { $(this).text("已推荐到首页"); });
    $("#Recommand").click(function () {
        var targetid = $(this).attr("targetid");
        var ObjectThis = $(this);
        var TargetType = $(this).attr("imagetype");
        $.ajax({
            url: $Maticsoft.BasePath + "Admin/AjaxRecommandOperation",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { Type: "recommand", TargetId: targetid, TargetType: TargetType },
            success: function (resultData) {
                if (resultData == "Yes") {
                    ObjectThis.hide().prev().show();
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });

    });

    $("#Delete").click(function () {
        $.jBox.confirm("确定要删除此专辑和专辑下面的商品或图片吗？", "提示", submitDelete);
       
    })


})

var submitDelete = function (v, h, f) {
    if (v == 'ok') {
        $.jBox.tip("正在删除数据...", 'loading');
        var ObjectThis = $("#Recommand");
        var targetid = ObjectThis.attr("targetid");
        var TargetType = ObjectThis.attr("imagetype");
        $.ajax({
            url: $Maticsoft.BasePath + "Admin/AjaxDeleteOperation",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { TargetId: targetid, TargetType: TargetType },
            success: function (resultData) {
                if (resultData == "Yes") {
                    $.jBox.tip('删除成功。', 'success');
                    window.location.href = "/Home/Index";
                }
                else {
                    $.jBox.tip('删除失败', 'error');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });



    }
    else if (v == 'cancel') {
        // 取消
    }
    return true; //close
};