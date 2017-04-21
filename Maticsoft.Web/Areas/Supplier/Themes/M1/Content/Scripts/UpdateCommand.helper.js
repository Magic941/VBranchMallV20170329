$(function () {
    $("#btnSave").click(function () {
        var Name = $("#Name").val();
        var dropAction = $("#dropAction").val();
        var txtSequence = $("#txtSequence").val();
        var ddParseType = $("#ddParseType").val();
        var txtParseType = $("#txtParseType").val();
        var Remark = $("#Remark").val();
        var CommandId = $("#CommandId").val();
        var status = "";
        if ($("#chkStatus").attr("checked") == "checked") {
            status = 1;
        } else {
            status = 0;
        }
        var ddTarget = $("#ddTarget").val();
        $.ajax({
            url: ($Maticsoft.BasePath + "WeChat/Update?timestamp={0}").format(new Date().getTime()),
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: { dropAction: dropAction, Name: Name, txtSequence: txtSequence, ddParseType: ddParseType, status: status, ddTarget: ddTarget, txtParseType: txtParseType, Remark: Remark, CommandId: CommandId },
            success: function (resultData) {
                if (resultData["Result"] == "OK") {
                    ShowSuccessTip("操作成功!");
                    setTimeout("DelayTime()", 2000);
                } else if (resultData["Result"] == "Action") {
                    ShowFailTip("请选择指定操作!");
                } else if (resultData["Result"] == "Name") {
                    ShowFailTip("请输入指令名称!");
                } else {
                    ShowFailTip("操作失败!");
                }
            }, error: function (xmlHttpRequest, textStatus, errorThrown) {
                ShowFailTip(xmlHttpRequest.responseText);
            }
        });
    });
    $("#ddTarget").attr("style", "display:none");
    $('#dropAction').click(function () {
        var ActionId = $("#dropAction").val();
        if (ActionId == "1") {
            $("#ddTarget").attr("style", "display:inline");
            $("#ddTarget").length = 0;
            $.ajax({
                url: ($Maticsoft.BasePath + "WeChat/ActionOne?timestamp={0}").format(new Date().getTime()),
                type: 'POST',
                dataType: 'json',
                timeout: 10000,
                async: false,
                data: { ActionId: ActionId },
                success: function (resultData) {
                    if (resultData["Status"] = "OK") {
                        var option = $('<option value="' + resultData["ClassID"] + '">' + resultData["ClassName"] + '</option>').appendTo($("#ddTarget"));
                    }
                }
            });
        } else if (ActionId == "2") {
            $("#ddTarget").attr("style", "display:inline");
            $.ajax({
                url: ($Maticsoft.BasePath + "WeChat/ActionTwo?timestamp={0}").format(new Date().getTime()),
                type: 'POST',
                dataType: 'json',
                timeout: 10000,
                async: false,
                data: { ActionId: ActionId },
                success: function (resultData) {
                    if (resultData.Status && resultData.Status == "OK" && resultData.DATA) {
                        $("#ddTarget").empty();
                        $(resultData.DATA).each(function () {
                            $('<option value="' + this.Value + '">' + this.Name + '</option>').appendTo($("#ddTarget"));
                        });
                    }
                }
            });
        } else {
            $("#ddTarget").attr("style", "display:none");
        }
    });
});
function DelayTime() {
    window.parent.location.reload();
}