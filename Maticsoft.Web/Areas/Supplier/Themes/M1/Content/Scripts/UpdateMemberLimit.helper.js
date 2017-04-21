
$(function () {
    $("#btnCancel").click(function () {
        window.parent.location.reload();
    });
    $("#btnSave").click(function () {
        var limitID = $("#LimitID");
        var limitName = $("#tName");
        var Cycle = $("#Cycle");
        var CycleUnit = $("#CycleUnit");
        var MaxTimes = $("#MaxTimes");
        if (!limitName.val()) {
            ShowFailTip("限制名称不能为空");
            return;
        }
        if (Cycle.val() < 1 || Cycle.val() > 100) {
            ShowFailTip("周期频率在1-100之间");
            return;
        }
        if (MaxTimes.val() < 1 || MaxTimes.val() > 100) {
            ShowFailTip("次数限制在1-100之间");
            return;
        }
        $.ajax({
            url: ($Maticsoft.BasePath + "Member/UpdateLimit?timestamp={0}").format(new Date().getTime()),
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: { limitID: limitID.val(), limitName: limitName.val(), Cycle: Cycle.val(), CycleUnit: CycleUnit.val(), MaxTimes: MaxTimes.val() },
            success: function (resultData) {
                if (resultData["Result"] == "Action") {
                    ShowFailTip("已存在该限制码，请重新填写");
                } else if (resultData["Result"] == "OK") {
                    ShowSuccessTip("修改成功!");
                    setTimeout("DelayTime()", 2000);
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
