$(function () {


    $("#SaveModify").click(function () {
        var txtName = $("#txtName").val();
        if (txtName.length == 0) {
            ShowFailTip("请输入商品名称！");
            return false;
        }
        var values=$('#Modify').serialize();
        values += '&Introduction=' + editor.getContent();
        $.ajax({
            url: ($Maticsoft.BasePath + "Home/Modify?timestamp={0}").format(new Date().getTime()),
            type: 'POST',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: values,
            success: function (resultData) {
                if (resultData["Result"] == "OK") {
                    ShowSuccessTip("修改成功,请等待管理员审核!");
                } else {
                    ShowFailTip("修改失败!");
                }
            }, error: function (xmlHttpRequest, textStatus, errorThrown) {
                ShowFailTip(xmlHttpRequest.responseText);
            }
        });
    });
});