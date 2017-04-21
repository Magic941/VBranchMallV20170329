function SubSave() {
    var txtPwd = $("#txtPwd");
    var txtNewPwd = $("#txtNewPwd");
    var txtConfirmPwd = $("#txtConfirmPwd");
    if (!txtPwd.val()) {
        ShowFailTip("请输入原密码");
        return;
    }
    if (!txtNewPwd.val()) {
        ShowFailTip("请输入新密码");
        return;
    }
    if (txtNewPwd.val() != txtConfirmPwd.val()) {
        ShowFailTip("两次密码不一致");
        return;
    }
    $.ajax({
        url: ($Maticsoft.BasePath + "Home/UserPass?timestamp={0}").format(new Date().getTime()),
        type: 'POST',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: { oldPassword: txtPwd.val(), newPassword: txtNewPwd.val(), confirmPassword: txtConfirmPwd.val() },
        success: function (resultData) {
            if (resultData["Result"] == "OK") {
                ShowSuccessTip("修改成功!");
            } else if (resultData["Result"] == "Error") {
                ShowFailTip("原密码错误!");
            } else if (resultData["Result"] == "ConfirmError") {
                ShowFailTip("两次密码输入不一致!");
            } else {
                ShowFailTip("修改失败!");
            }
        }, error: function (xmlHttpRequest, textStatus, errorThrown) {
            ShowFailTip(xmlHttpRequest.responseText);
        }
    });
}