var phoneStatus = true;
var pwd1Status = true;
var rePwd1Status = true;
var checkCodeStatus = true;
var agreeLaw = true;
var agreementStatus1 = true;
var phoneValidateOnce = {
    Phone: "",
    Exists: false
};

var iCount = 2;
showReGetChekcCodeMsg();

function showReGetChekcCodeMsg() {
    iCount--;
    if (iCount <= 0) {
        var abtnregetcheckcode = $('#abtnregetcheckcode');
        abtnregetcheckcode.attr('class', 'btn h');
        abtnregetcheckcode.css('cursor', 'pointer');
        $('#phone').attr("disabled", "");
        $('#spanregetcheckcodemsg').text('免费获取校验码');
        abtnregetcheckcode.bind('click',
        function () {
            CheckPhone($('#phone'));
            if (!phoneStatus) {
                //alert("请填写有效的手机号码作为登录用户名");
                $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写有效的手机号码作为登录用户名</p>");
                return;
            }
            // 发送校验码
            var params = {
                MobilePhone: $("#phone").val()
            };
            $.ajax({
                type: "GET",
                dataType: "json",
                url: "/Service/ContactService.ashx?Method=RegisterByPhoneSendCheckCode",
                data: params,
                success: function (result) {
                    if (result.msgCode != 0 && result.msgCode != 3) {
                        //if () {
                        //alert(phoneSendSmsCountOverLimitTip);
                        //} else {
                        //alert(result.msgStr);
                        //}
                        iCount = 1;
                        return;
                    } else {
                        // 操作成功和操作操作超过限制都显示成功
                        $("#CheckCodeSuccessTip").removeClass("none");
                    }
                }
            });

            iCount = 61;
            abtnregetcheckcode.unbind("click");
            abtnregetcheckcode.attr('class', 'btn btn-code');
            abtnregetcheckcode.css('cursor', 'default');
            $('#phone').attr("disabled", "disabled");
            showReGetChekcCodeMsg();
        });
        return;
    }
    /*
    else if (i == 2) {
    $("#CheckCodeSuccessTip").addClass("none");
    }
    */
    $('#spanregetcheckcodemsg').text(iCount + '秒后 重新获取校验码');
    setTimeout("showReGetChekcCodeMsg()", 1000);
}

$(document).ready(function () {

    // 手机号码文本框获取焦点事件
    $("#phone").focus(function () {
        $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写有效的手机号码作为登录用户名</p>");
    });

    // 输入密码文本框获取焦点事件
    $("#pwd1").focus(function () {
        $("#divPwd1Tip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>" + focusmsg + "</p>");
    });

    // 重新输入密码文本框获取焦点事件
    $("#repwd1").focus(function () {
        $("#divrePwd1Tip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请再次填写密码,两次输入的密码要求一致</p>");
    });

    // 校验码文本框获取焦点事件
    $("#checkCode").focus(function () {
        $("#divCheckCodeTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写6位数字校验码</p>");
    });

    $("#phone").blur(function () {
        CheckPhone($(this));
    });
    $("#pwd1").blur(function () {
        CheckPwd1($(this));
    });
    $("#repwd1").blur(function () {
        CheckRePwd1($(this));
    });
    $("#checkCode").blur(function () {
        CheckCheckCode($(this));
    });

    $('#phone').keypress(function (event) {
        if (event.which == 13) {
            $('#pwd1').focus();
            return false;
        }
    });

    $('#pwd1').keypress(function (event) {
        if (event.which == 13) {
            $('#repwd1').focus();
            return false;
        }
    });

    $('#repwd1').keypress(function (event) {
        if (event.which == 13) {
            $('#checkCode').focus();
            return false;
        }
    });

    $('#checkCode').keypress(function (event) {

        if (event.which == 13) {
            $('#phoneRegisterSubmit').click();
            return false;
        }
    });

    $("#phoneRegisterSubmit").click(function () {
        PhoneRegisterSubmit();
    });

    $("#chkAgreement1").click(function () {
        CheckAgreement1($(this));
    })

});

function PhoneRegisterSubmit() {
    if (SubmitCheck()) {
        $("#phoneRegisterSubmit").unbind("click");
        // 提交信息
        var params = {
            MobilePhone: $("#phone").val(),
            pwd: $("#pwd1").val(),
            checkCode: $("#checkCode").val(),
            Url: phoneRegisterReturnUrl
        };
        $.ajax({
            type: "GET",
            dataType: "json",
            url: "/Service/LoginV2.ashx?Method=RegisterByPhone",
            data: params,
            error: function () {
                $("#phoneRegisterSubmit").click(function () {
                    PhoneRegisterSubmit();
                });
                window.location.href = "UserRegisterByPhoneResult.aspx";
            },
            success: function (result) {
                //alert(result.msgCode);
                switch (result.msgCode) {
                    case 0:
                        // 注册成功,跳转至手机注册结果页面
                        window.location.href = result.returnUrl;
                        break;
                    case 25:
                        // 注册失败,跳转到手机注册结果页面
                        window.location.href = "UserRegisterByPhoneResult.aspx";
                        break;
                    case 30:
                        // 密码长度不正确
                        $('#divPwd1Tip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>" + errormsg + "</p>");
                        break;
                    case 31:
                        // 手机号码格式不正确
                        $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写有效的手机号码</p>");
                        break;
                    case 32:
                        // 手机号码已被注册或绑定
                        $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>该号码已被注册,请使用其它手机号码注册</p>");
                        break;
                    case 33:
                        // 校验码格式不正确
                        $('#divCheckCodeTip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>校验码必须是六位数字</p>");
                        break;
                    case 34:
                        // 校验码不正确
                        $('#divCheckCodeTip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>校验码无效，请重新获取</p>");
                        break;
                    case 35:
                        // 校验码过期
                        $('#divCheckCodeTip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>校验码无效，请重新获取</p>");
                        break;
                    case 39:
                        // 校验码过期
                        $('#divCheckCodeTip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>注册成功后,登录时失败</p>");
                        break;
                    default:
                        // 跳转至失败页面
                        window.location.href = "UserRegisterByPhoneResult.aspx";
                        break;
                }
                $("#phoneRegisterSubmit").click(function () {
                    PhoneRegisterSubmit();
                });
            }
        });
    }
}

// 检查手机
function CheckPhone(obj) {
    if (obj.val().length == 0) {
        $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写有效的手机号码作为登录用户名</p>");
        phoneStatus = false;
    } else if (!checkMyMobile(obj.val())) {
        $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写有效的手机号码</p>");
        phoneStatus = false;
    } else {
        // 调用检查手机号码是否是注册账户,或有没有绑定,或有没有在基本资料中
        currentPhone = $("#phone").val();
        if (phoneValidateOnce.Phone == currentPhone) {
            if (phoneValidateOnce.Exists) {
                $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>该号码已被注册,请使用其它手机号码注册。使用该号码<a href='http://login.maticsoft'>登录</a>,忘记密码请点击<a href='http://login.maticsoft.com/contact/contactgetpwd.aspx'>找回密码</a></p>");
                phoneStatus = false;
            } else {
                // 手机号码不存在
                $("#divPhoneTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
                phoneStatus = true;
            }
            return;
        }
        phoneValidateOnce.Phone = currentPhone;
        var params = {
            MobilePhone: currentPhone
        };

        $.ajax({
            type: "POST",
            dataType: "json",
            url: "/Service/ContactService.ashx?Method=RegisterByPhoneCheckPhone",
            data: params,
            success: function (result) {
                if (result.msgCode == 0) {
                    // 手机号码不存在
                    $("#divPhoneTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
                    phoneStatus = true;
                    phoneValidateOnce.Exists = false;
                } else {
                    $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>该号码已被注册,请使用其它手机号码注册。使用该号码<a href='http://login.maticsoft'>登录</a>,忘记密码请点击<a href='http://login.maticsoft.com/contact/contactgetpwd.aspx'>找回密码</a></p>");
                    phoneStatus = false;
                    phoneValidateOnce.Exists = true;
                }
            },
            error: function () {
                $("#divPhoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>检查过程中错误,请联系客服</p>");
                phoneStatus = false;
            }
        });

    }

}

// 检查输入的密码
function CheckPwd1(obj) {
    if (obj.val().length == 0) {
        $('#divPwd1Tip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写密码</p>");
        pwd1Status = false;
    } else if (!regs.test(obj.val())) {
        $('#divPwd1Tip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>" + errormsg + "</p>");
        pwd1Status = false;
    } else {
        $('#divPwd1Tip').removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
        pwd1Status = true;
    }
}

// 检查再次输入的密码
function CheckRePwd1(obj) {
    if (obj.val().length == 0) {
        $('#divrePwd1Tip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请再次填写密码,两次输入的密码要求一致</p>");
        rePwd1Status = false;
    } else if (obj.val() != $('#pwd1').val()) {
        $('#divrePwd1Tip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>两次填写的不一致，请重新填写</p>");
        rePwd1Status = false;
    } else {
        $('#divrePwd1Tip').removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
        rePwd1Status = true;
    }
}

// 检查校验码
function CheckCheckCode(obj) {
    // 判断校验码格式是否正确
    re = /^\d+(\.\d+)?$/;
    if (obj.val().length == 0) {
        $('#divCheckCodeTip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写6位数字校验码</p>");
        checkCodeStatus = false;
    } else if (obj.val().length != 6 || !re.test(obj.val())) {
        $('#divCheckCodeTip').removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请填写6位数字校验码</p>");
        checkCodeStatus = false;
    } else {
        $('#divCheckCodeTip').removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("</i><p>&nbsp;</p>");
        checkCodeStatus = true;
    }
}

function CheckAgreement1(obj) {
    if (obj.attr("checked") == true) {
        $("#divAgreementTip1").removeClass("msg msg-err").removeClass("msg msg-info").html("");
        agreementStatus1 = true;
    } else {
        $("#divAgreementTip1").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请先阅读并同意《用户服务协议》</p>");
        agreementStatus1 = false;
    }
}

// 提交前检查
function SubmitCheck() {

    CheckPhone($("#phone"));
    CheckPwd1($("#pwd1"));
    CheckRePwd1($("#repwd1"));
    CheckCheckCode($("#checkCode"));
    CheckAgreement1($("#chkAgreement1"));
    if (phoneStatus && pwd1Status && rePwd1Status && checkCodeStatus && agreementStatus1) {
        return true;
    } else {
        return false;
    }
}

// 检查手机格式是否正确
function checkMyMobile(mobileNo) {
    var reg0 = /^13\d{9}$/;
    var reg1 = /^15\d{9}$/;
    var reg2 = /^18\d{9}$/;
    var reg3 = /^14\d{9}$/;

    var my = false;
    if (reg0.test(mobileNo)) my = true;
    else if (reg1.test(mobileNo)) my = true;
    else if (reg2.test(mobileNo)) my = true;
    else if (reg3.test(mobileNo)) my = true;
    else my = false;
    return my;
}