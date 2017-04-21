$(function () {
    $("#btnCancel").click(function () {
        window.parent.location.reload();
    });
    $("#btnSave").click(function () {
        var limitName = $("#limitName");
        var Cycle = $("#Cycle");
        var CycleUnit = $("#CycleUnit");
        var MaxTimes = $("#MaxTimes");
        if (!limitName.val()) {
            ShowFailTip("限制名称不能为空");
            return;
        }
        $.ajax({
            url: ($Maticsoft.BasePath + "Member/AddLimit?timestamp={0}").format(new Date().getTime()),
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: { limitName: limitName.val(), Cycle: Cycle.val(), CycleUnit: CycleUnit.val(), MaxTimes: MaxTimes.val() },
            success: function (resultData) {
                if (resultData["Result"] == "Action") {
                    ShowFailTip("已存在该限制码，请重新填写");
                } else if (resultData["Result"] == "OK") {
                    ShowSuccessTip("添加成功!");
                    setTimeout("DelayTime()", 2000);
                } else {
                    ShowFailTip("添加失败!");
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
