$(function () {
    //手机号
    $("#UserName").blur(function () {usernamecheck(); })
    //手机验证码
    $("#SMSCode").blur(function () { smscodecheck(); })
    //手机密码
    $("#password").blur(function () { pwdcheck(); })
    //重复密码
    $("#ConfirmPassword").blur(function () { comfirmpwdcheck(); })

    //获取短信验证码设置
    var wait = 120;
    var get_code_time = function (o) {
        if (wait == 0) {
            o.attr("disabled", false);
            o.text("免费获取验证码");
            wait = 120;
        } else {
            o.attr("disabled", true);
            o.text("(" + wait + ")秒后重新获取");
            wait--;
            setTimeout(function () {
                get_code_time(o)
            }, 1000)
        }
    }

    //点击发送短信
    $(".ster_ma").click(function () {
        if (!(/(^1[3|5|8][0-9]{9}$)/).test($("#UserName").val())) {
            ShowFailTip("手机号码不正确！");
        }
        var txtCodel = $("#CheckCode").val();
        if (txtCodel == "" || txtCodel == null) {
            ShowFailTip("请输入图片验证码！");
            return false;
        }
        else {
            //验证手机号码是否已经注册
            $.ajax({
                url: '/Account/IsExistPhone',
                type: 'post',
                data: { phone: $("#UserName").val() },
                success: function (data) {
                    if (data) {
                        ShowFailTip("该手机号不存在！");
                    }
                    else {
                        get_code_time($(".ster_ma"));
                        $.ajax({
                            url: '/Account/SendSMSForPwd',
                            type: 'post',
                            async: false,
                            data: { phone: $("#UserName").val(), Code: txtCodel },
                            success: function (data) {
                                if (data == "SmsValCode") {
                                    $("#divPhoneTip").removeClass("red").addClass("tipClass").html("请输入正确的图片验证码!");
                                    phoneStatus = false;
                                } else if (data == "True") {
                                    get_code_time($(".ster_ma"));
                                } else {
                                    $("#divPhoneTip").removeClass("red").addClass("tipClass").html("服务器没有返回数据，可能服务器忙，请稍候再试！");
                                    phoneStatus = false;
                                }
                            }
                        });
                    }
                }
            });
        }
    })

})

//手机号码验证
function usernamecheck() {
    var status;
    if (!(/(^1[3|5|8][0-9]{9}$)/).test($("#UserName").val())) {
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
                    CheckFailed("usernameTip", "*该手机号不存在！")
                    status = false;
                }
                else {
                    CheckSuccess("usernameTip");
                    status = true;
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

//手机短信验证
function smscodecheck() {
    var status;
    if (!$("#SMSCode").val()) {
        CheckFailed("smscodeTip", "*手机验证码不正确");
        status = false;
    }

    $.ajax({
        url: '/Account/VerifiyCode',
        type: 'post',
        async: false,
        data: { SMSCode: $("#SMSCode").val() },
        success: function (resultData) {
            if (resultData == "False") {
                CheckFailed("smscodeTip", "*手机验证码不正确");
                status = false;
            } else {
                CheckSuccess("smscodeTip");
                status = true;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            CheckFailed("smscodeTip", "*服务器没有返回数据，可能服务器忙，请稍候再试！");
            status = false;
        }
    })
    return status;
}
//======================================================================================================================================================

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
        CheckFailed("pwdTip", "*密码6-30位，支持“数字、字母");
        return false;
    }
    else {
        CheckSuccess("pwdTip");
        return true;
    }
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