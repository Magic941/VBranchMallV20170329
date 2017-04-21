function f_ExistEMail() {
    var email = document.all("txtEMail");

    if (!f_StringCheck(email, "EMail", false, 40)) return false;
    if (!f_CheckEMail(email.value)) {
        window.alert("EMail格式不正确");
        email.focus();
        return false;
    }
    //document.all.hidActionType.value = "email";
    document.frames("fraEMail").document.all.hidEMail.value = email.value;
    document.frames("fraEMail").document.forms(0).submit();

    return false;
}
function f_EMailExists(bExists) {
    if (bExists) alert("email地址已经存在，请使用其他的email");
    else alert("email地址不存在，可以使用");
}

function f_CheckForm() {
    var txtEMail = document.all("txtEMail");
    txtEMail.Text = "用户名/电子邮件";
    var txtConfirmEMail = document.all("txtConfirmEMail");
    var txtPassword = document.all("txtPassword");
    txtPassword.Text = "密码";
    var txtConfirmPwd = document.all("txtConfirmPwd");

    if (!f_StringCheck(txtEMail, "用户名/电子邮件", false, 40)) return;
    if (!f_CheckEMail(txtEMail.value)) {
        Alert(txtEMail, "格式不正确");
        return;
    }
    if (txtEMail.value != txtConfirmEMail.value) {
        alert("您两次输入的电子邮件不同!");
        txtEMail.focus();
        return;
    }

    var reg = /^(\w|@|#|\$){6,30}/;
    if (!reg.test(txtPassword.value)) {
        Alert(txtPassword, "格式不正确");
        return
    }
    if (txtPassword.value != txtConfirmPwd.value) {
        alert("重复密码与密码不一致");
        txtConfirmPwd.focus();
        return;
    }
    //if ( document.all("tr_VerifyRegister").style.display!="none")
    //{
    //	if( !f_CheckRegVerify())
    //	{
    //		return ;
    //	}
    //}
    document.forms[0].submit();
}
function Alert(obj, alertStr) {
    alert(obj.Text + alertStr);
    obj.focus();
    return false;
}
function f_kdnForm() {
    if (event.keyCode != 13) return;
    var eventid = window.event.srcElement.id;
    if (eventid == 'txtEMail' || eventid == 'txtPassword' || eventid == 'txtConfirmEMail' || eventid == 'txtConfirmPwd' || eventid == 'VerifyControl1__ctl0_txt_Code') {
        f_CheckForm();
        return false;
    }

    return false;
}