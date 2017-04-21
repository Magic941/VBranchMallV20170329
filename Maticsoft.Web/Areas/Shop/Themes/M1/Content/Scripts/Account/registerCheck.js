$(function () {
    //手机号
    $("#UserName").blur(function () { $("#NickName").val($(this).val()); usernamecheck(); })
    //手机验证码
    $("#SMSCode").blur(function () { smscodecheck(); })
    //手机密码
    $("#password").blur(function () { pwdcheck(); })
    //重复密码
    $("#ConfirmPassword").blur(function () { comfirmpwdcheck();})
})

function usernamecheck() {
    var status;
    if (!(/(^1[7|3|5|8][0-9]{9}$)/).test($("#UserName").val())) {
        CheckFailed("usernameTip", "*手机号码不正确")
        status = false;
    }
    else {
        $.ajax({
            url: $Maticsoft.BasePath + "Account/IsExistPhone",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false,
            data: {
                Action: "post", phone: $("#UserName").val()
            },
            success: function (resultData) {
                
                if (resultData == "true") {
                    CheckSuccess("usernameTip");
                    status = true;
                }
                else {
                    CheckFailed("usernameTip", "*该手机号已经注册")
                    status = false;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                status = false;
            }

        });
        
        return status;
    }

}

function comfirmpwdcheck() {
    if ($("#ConfirmPassword").val() == "") {
        CheckFailed("comfirmpwdTip", "*确认密码不能为空")
        return false;
    }
    if ($("#ConfirmPassword").val() != $("#password").val()) {
        CheckFailed("comfirmpwdTip", "*两次密码输入不相同")
        return false;
    }
    else {
        CheckSuccess("comfirmpwdTip");
        return true;
    }
}

function pwdcheck() {
    if (!(/^[a-zA-Z0-9]{6,30}$/).test($("#password").val())) {
        CheckFailed("pwdTip", "*密码6-30位，支持数字、字母");
        return false;
    }
    else {
        CheckSuccess("pwdTip");
        return true;
    }
}

function smscodecheck() {
    var status;
    if (!$("#SMSCode").val()) {
        CheckFailed("smscodeTip", "*手机验证码不正确");
        status= false;
    }

    $.ajax({
        url: '/Account/VerifiyCode',
        type: 'post',
        async:false,
        data: { SMSCode: $("#SMSCode").val() },
        success: function (resultData) {
            if (resultData == "False") {
                CheckFailed("smscodeTip", "*手机验证码不正确");
                status= false;
            } else {
                CheckSuccess("smscodeTip");
                status= true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            CheckFailed("smscodeTip", "*服务器没有返回数据，可能服务器忙，请稍候再试！");
            status= false;
        }
    })
    return status;
}


function CheckFailed(id, msg) {
    $("#" + id).show().html("<span style=\"color:red\">" + msg + "</span>");
}

function CheckSuccess(id) {
    $("#" + id).hide();
}

function CheckForm()
{
    if (usernamecheck() && smscodecheck() && pwdcheck() && comfirmpwdcheck()) {
        return true;
    }
    else {
        ShowFailTip("请完善信息！");
        return false;
        
    }
}