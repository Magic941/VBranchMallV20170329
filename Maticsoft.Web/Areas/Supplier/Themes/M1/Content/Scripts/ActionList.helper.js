function SaveGroup() {
    var tName = $("#ActionName");
    if (!tName.val()) {
        ShowFailTip("系统指令名称不能为空");
        return;
    }
    $.ajax({
        url: ($Maticsoft.BasePath + "WeChat/ActionList?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: $('#actionList').serializeArray(),
        success: function (resultData) {
            if (resultData["Result"] == "OK") {
                ShowSuccessTip("添加成功!");
                window.location.reload();
            } else {
                ShowFailTip("添加失败!");
            }      
        }, error: function (xmlHttpRequest, textStatus, errorThrown) {
            ShowFailTip(xmlHttpRequest.responseText);
        }
    });
}