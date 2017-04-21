function SaveGroup() {
    var tName = $("#GroupName");
    if (!tName.val()) {
        ShowFailTip("分组名称不能为空");
        return;
    }
    $.ajax({
        url: ($Maticsoft.BasePath + "WeChat/GroupList?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: $('#btnModifyGroup').serializeArray(),
        success: function (resultData) {
            if (resultData["Result"] == "OK") {
                ShowSuccessTip("添加成功!");
                setTimeout("DelayTime()", 2000);
            } else {
                ShowFailTip("添加失败!");
            }
        }, error: function (xmlHttpRequest, textStatus, errorThrown) {
            ShowFailTip(xmlHttpRequest.responseText);
        }
    });
}
function DelayTime() {
    window.location.reload();
}