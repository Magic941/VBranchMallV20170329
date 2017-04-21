var registerType = 'Mail';
var regs = /^[A-Za-z0-9]{6,30}$/;
var focusmsg = '请填写密码（6-30位数字或字母）';
var errormsg = '密码6-30位，支持“数字、字母”';
var mailStatus = true;
var nicknameStatus = true;
var pwdStatus = true;
var codeStatus = false;
var phoneStatus = false;
var vpwdStatus = true;
var agreementStatus = true;
var checkOK = true;
var validateOnce = {
    Email: "",
    Exists: false
};

$(function () {
    var regStr = $('#hfRegisterToggle').val(); //注册方式
    var isOpen = $("#hfSMSIsOpen").val();
    if (regStr == 'Phone') {
        if (isOpen == "True") {
            $(".txtphone").show();
        }
    }
    //注册按钮
    $("#btnEmailRegister").click(function () {
        $("#divRegTip").removeClass().html("");
        if (regStr == 'Phone') {
            if (!codeStatus && isOpen == "True") {
                ShowFailTip("手机效验码不正确");
                return;
            }
        }
        if (CheckRegister()) {
            $("#registerSubmit").trigger("click");
        }
    });

    $("#btnSendSMS").click(function () {
        CheckPhone($("#phone"));
        var phone = $("#phone").val();
        var txtCodel = $("#CheckCode").val();
        if (phone == "") {
            ShowFailTip("请输入手机号码");
            return;
        }
        if (txtCodel == "" || txtCodel == null) {
            ShowFailTip("请输入图片验证码");
            return;
        } 


        if (phoneStatus) {
            //发送短信
            $.ajax({
                url: $Maticsoft.BasePath + "Account/SendSMS",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false,
                data: {
                    Action: "post", Phone: phone, SmsValCode: txtCodel
                },
                success: function (resultData) {
                    if (resultData == "True") {
                        ShowSuccessTip("发送短信成功");
                        smsSeconds = 60;
                        //$("#btnSendSMS").attr("value", "请在(" + smsSeconds + ")秒后重新发送");

                        intervaSMS = setInterval("CountDown()", 1000);
                    }
                    else {
                        if (resultData == "SmsFalse") {
                            $("#divPhoneTip").removeClass("red").addClass("tipClass").html("请输入正确的图片验证码!");
                            phoneStatus = false;
                        } else if (resultData == "True")
                        {
                            return true;
                        }
                        else {
                            $("#divPhoneTip").removeClass("red").addClass("tipClass").html("服务器没有返回数据，可能服务器忙，请稍候再试！");
                            phoneStatus = false;
                        }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                    phoneStatus = false;
                }

            });
            $("#btnVerify").click();
        }
    });

    $("#checkCode").blur(function () {
        var code = $(this).val();
        if (code == "") {
            ShowFailTip("请输入手机效验码");
            return;
        }
        $.ajax({
            url: $Maticsoft.BasePath + "Account/VerifiyCode",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false,
            data: {
                Action: "post", SMSCode: code
            },
            success: function (resultData) {

                if (resultData == "False") {
                    ShowFailTip("手机效验码不正确");
                    codeStatus = false
                } else {
                    $("#divVerifyCodeTip").removeClass("red").addClass("tipClass").html("");
                    codeStatus = true;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                mailStatus = false;
            }

        });
    });
    //微信新用户绑定
    $("#btnRegBind").click(function () {
        if (CheckRegister()) {
            $(this).attr("disabled", "disabled");
            var eamil = $("#email").val();
            var pwd = $("#pwd").val();
            var nick = $("#nickname").val();
            var user = $("#txtUser").val();
            var open = $("#txtOpenId").val();
            $.ajax({
                url: $Maticsoft.BasePath + "Account/AjaxRegBind",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false,
                data: {
                    Action: "post", UserName: eamil, UserPwd: pwd, NickName: nick, User: user, OpenId: open
                },
                success: function (resultData) {

                    if (resultData == "1") {
                        ShowSuccessTip("绑定用户成功！");
                    }
                    if (resultData == "3") {
                        ShowFailTip("该账户已经绑定了其它帐号！");
                    }
                    if (resultData == "0") {
                        ShowFailTip("服务器繁忙，请稍候再试！");
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowServerBusyTip("服务器繁忙，请稍候再试！");
                }

            });
        }
    });

    $("#email").focus(function () {
        $("#divEmailTip").removeClass("red").addClass("tipClass").html("请填写有效的Email地址作为登录用户名。");
    }).blur(function () {
        CheckEmail($(this));
    });

    $("#nickname").focus(function () {
        $("#divNicknameTip").removeClass("red").addClass("tipClass").html("请填写昵称！");
    }).blur(function () {
        CheckNickname($(this));
    });

    $("#pwd").focus(function () {
        $("#divPwdTip").removeClass("red").addClass("tipClass").html(focusmsg);
    }).blur(function () {
        CheckPwd($(this));
    });
    $("#vpwd").focus(function () {
        $("#divVPwdTip").removeClass("red").addClass("tipClass").html("请再次填写密码，两次输入必须一致");
    }).blur(function () {
        CheckVPwd($(this));
    });

    $("#phone").focus(function () {
        $("#divPhoneTip").removeClass(" tipClass").addClass("red").html("请填写手机号码");
    }).keypress(function (event) {
        if (event.which == 13) {
            $("#btnEmailRegister").trigger("click");
        }
    }).blur(function () {
        CheckPhone($(this));
    });

    $("#chkAgreement").click(function () {
        CheckAgreement($(this));
    });
});

function CheckRegister() {
    //    var isOpen = $("#hfSMSIsOpen").val();
    //    if (isOpen != "True") {
    //        CheckEmail($("#email"));
    //    }
    var regStr = $('#hfRegisterToggle').val();
    var userNameStatus;
    if (regStr == "Phone") {
        CheckPhone($("#phone"));
        userNameStatus = phoneStatus;
    } else {
        CheckEmail($("#email"));
        userNameStatus = mailStatus;
    }
    CheckNickname($("#nickname"));
    CheckPwd($("#pwd"));
    CheckVPwd($("#vpwd"));
    CheckAgreement($("#chkAgreement"));
    if (!userNameStatus || !pwdStatus || !vpwdStatus || !nicknameStatus || !agreementStatus) {
        checkOK = false;
    } else {
        checkOK = true;
    }
    return checkOK;
}

//验证邮箱
function CheckEmail(obj) {
    var regs = /^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    var emailval = obj.val();
    if (emailval != "") {
        if (!regs.test(emailval)) {
            $("#divEmailTip").removeClass("tipClass").addClass("red").html("请填写有效的Email地址");
            mailStatus = false;
        } else {
            //验证注册邮箱是否存在
            $.ajax({
                url: $Maticsoft.BasePath + "Account/IsExistUserName",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false,
                data: {
                    Action: "post", userName: emailval
                },
                success: function (resultData) {
                    if (resultData == "true") {
                        $("#divEmailTip").removeClass("red").addClass("tipClass").html("&nbsp;");
                        mailStatus = true;
                    }
                    else {
                        $("#divEmailTip").removeClass("tipClass").addClass("red").html("该Email已被注册，请使用其他Email地址注册。使用该地址<a href='" + $Maticsoft.BasePath + "Account/Login'>登录</a></p>");
                        mailStatus = false;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                    mailStatus = false;
                }

            });
        }
    } else {
        $("#divEmailTip").removeClass("tipClass").addClass("red").html("请填写有效的Email地址作为登录用户名");
        mailStatus = false;
    }
    return;
}
function CheckPhone(obj) {
    var regs = /^1([34578][0-9]|4[57]|5[^4])\d{8}$/;
    var phoneval = obj.val();
    if (phoneval != "") {
        if (!regs.test(phoneval)) {
            $("#divPhoneTip").removeClass("tipClass").addClass("red").html("请填写有效的手机号码");
            phoneStatus = false;
            return;
        } else {
            //验证手机是否存在
            $.ajax({
                url: $Maticsoft.BasePath + "Account/IsExistUserName",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false,
                data: {
                    Action: "post", userName: phoneval
                },
                success: function (resultData) {
                    if (resultData == "true") {
                        $("#divPhoneTip").html("");
                        phoneStatus = true;
                    }
                    else {
                        $("#divPhoneTip").removeClass("tipClass").addClass("red").html("该手机号码已被注册，请使用其他手机号码注册。");
                        phoneStatus = false;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                    phoneStatus = false;
                }

            });
        }
    } else {
        phoneStatus = false;
    }
    return;
}
//验证昵称
function CheckNickname(obj) {
    var i = 0;
    var niclnamevalue = obj.val();
    if (niclnamevalue.indexOf(";") > -1) {
        $("#divNicknameTip").removeClass("red").addClass("tipClass").html('用户名不能包含“；”');  //ShowFailTip('大神，请您手下留情！');
        $(this).val("");
        i++;
        if (i >= 3) {
            $("#divNicknameTip").removeClass("red").addClass("tipClass").html('别玩了，这样有意思吗？'); //ShowFailTip('别玩了，这样有意思吗？');
        }
        nicknameStatus = false;
        return;
    }
    if (niclnamevalue != "") {
        //验证昵称是否存在
        $.ajax({
            url: $Maticsoft.BasePath + "Account/IsExistNickName",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                nickName: niclnamevalue
            },
            success: function (resultData) {
                if (resultData == "true") {
                    $("#divNicknameTip").removeClass("red").addClass("tipClass").html("&nbsp;");
                    nicknameStatus = true;
                } else {
                    $("#divNicknameTip").removeClass("tipClass").addClass("red").html("该昵称已被其他用户抢先使用，换一个试试");
                    nicknameStatus = false;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                nicknameStatus = false;
            }
        });
    } else {
        $("#divNicknameTip").removeClass("tipClass").addClass("red").html("请填写昵称！");
        nicknameStatus = false;
    }
    return;
}

//验证密码
function CheckPwd(obj) {
    var pwdval = obj.val();
    if (pwdval.length == 0) {
        $("#divPwdTip").removeClass("tipClass").addClass("red").html("请填写密码");
        pwdStatus = false;
        return;
    }
    if (!regs.test(pwdval)) {
        $("#divPwdTip").removeClass("tipClass").addClass("red").html(errormsg);
        pwdStatus = false;
    } else {
        $("#divPwdTip").removeClass("red").addClass("tipClass").html("&nbsp;");
        pwdStatus = true;
    }
}

//验证确认密码
function CheckVPwd(obj) {
    if (obj.val() != "") {
        if (obj.val() != $("#pwd").val()) {
            $("#divVPwdTip").removeClass("tipClass").addClass("red").html("两次填写的不一致，请重新填写");
            vpwdStatus = false;
        } else {
            $("#divVPwdTip").removeClass("red").addClass("tipClass").html("&nbsp;");
            vpwdStatus = true;
        }
    } else {
        $("#divVPwdTip").removeClass("tipClass").addClass("red").html("请再次填写密码，两次输入必须一致");
        vpwdStatus = false;
    }
}

//验证协议
function CheckAgreement(obj) {
    if (obj.attr("checked")) {
        $("#divAgreementTip").removeClass("msg msg-err").removeClass("msg msg-info").html("");
        agreementStatus = true;
    } else {
        $("#divAgreementTip").removeClass("tipClass").addClass("red").html("请先阅读并同意《用户服务协议》");
        agreementStatus = false;
    }
}

function CountDown() {
    if (smsSeconds <= 0) {
        //                $("[id$='txtPhone']").removeAttr("disabled");
        isOK = true;
        $("#btnSendSMS").attr("value", "重新获取验证码");
        $("#btnSendSMS").removeAttr("disabled");

        clearInterval(intervaSMS);
    } else {
        $("#btnSendSMS").attr("value", "请在(" + smsSeconds + ")秒后重新发送");
        $("#btnSendSMS").attr("disabled", "disabled");
        //                $("[id$='txtPhone']").attr("disabled", "disabled");
        isOK = false;
        smsSeconds--;
    }
}
