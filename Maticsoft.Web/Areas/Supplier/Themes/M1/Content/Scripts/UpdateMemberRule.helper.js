$(function () {
    $("#btnCancel").click(function () {
                window.location.href = $Maticsoft.BasePath + "Member/PointsRule";
        //window.parent.location.reload();
    });
    $("#btnSave").click(function () {
        var ruleID = $("#ruleID");
        var ActionName = $("#ActionName");
        var tName = $("#tName");
        var Score = $("#Score");
        var LimitName = $("#LimitName");
        var Remark = $("#Remark");
        if (!tName.val()) {
            ShowFailTip("规则名称不能为空");
            return;
        }
        var reg = /^\d+$/;
        if (!(Score.val().match(reg))) {
            ShowFailTip("只能输入整数!");
            return;
        }
        if (Score.val() < 0 || Score.val() > 1000) {
            ShowFailTip("数字只能在0-1000之间!");
            return;
        }
        $.ajax({
            url: ($Maticsoft.BasePath + "Member/UpdateRule?timestamp={0}").format(new Date().getTime()),
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: { ruleID: ruleID.val(), ActionName: ActionName.val(), tName: tName.val(), Score: Score.val(), LimitName: LimitName.val(), Remark: Remark.val() },
            success: function (resultData) {
                if (resultData["Result"] == "OK") {
                    ShowSuccessTip("修改成功!");
                                      window.location.href = $Maticsoft.BasePath + "Member/PointsRule";
                    //  setTimeout("DelayTime()", 2000);
                } else {
                    ShowFailTip("修改失败!");
                }
            }, error: function (xmlHttpRequest, textStatus, errorThrown) {
                ShowFailTip(xmlHttpRequest.responseText);
            }
        });
    });
});
function DelayTime() {
    window.parent.location.reload();
}
