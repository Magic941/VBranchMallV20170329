var registerType = 'Mail';
var regs = /^[A-Za-z0-9]{6,30}$/;
var focusmsg = '请填写密码（6-30位数字或字母）';
var errormsg = '密码6-30位，支持“数字、字母”';
var mailStatus = true;
var nicknameStatus = true;
var pwdStatus = true;
var vpwdStatus = true;
var phoneStatus = true;
var codeStatus = false;
var agreementStatus = true;
var checkOK = true;

var isOK = true;
var smsSeconds = 60;
var intervaSMS;
var validateOnce = {
    Email: "",
    Exists: false
};

$(function () {
   




    $(".bon_anza").hide();
    $("#UserName").blur(function () {
      CheckEmail($(this))
        
    });

    $("#reg_base").click(function () {
        $("#NickName").val($("#UserName").val());
        if (!$("#Chk").attr("checked")) { alert("请同意《好邻网使用协议》！"); return }
        if (!CheckEmail($("#UserName"))) {
            ShowFailTip("请填写邮箱！");
        }
        else {
            $("#submit").click();
            ShowSuccessTip("验证链接已发送至您的邮箱，请登录您的邮箱进行验证");
        }
    })

    $("#pwd").blur(function () { checkpwd(); })

    $("#pwdComfirm").blur(function () { comfirmcheck(); });

    $("#reg_pwd").click(function () {
        
        if (!checkpwd() && !comfirmcheck()) {
            ShowFailTip("请填写邮箱！");
        }
        else {

            $.ajax({
                url: $Maticsoft.BasePath + "Account/FillPassword",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false,
                data: {
                    SecretKey: $("#SecretKey").val(), password: $("#pwd").val(), Email: $("#email").val()
                },
                success: function (data) {
                    var obj = eval("(" + data + ")");
                    if (obj.success) {
                        $("#EmailName").text($("#email").val());
                        $(".bon_anza").show(1000);
                    } else { alert(obj.msg); }
                }
            });
        }
    })
});

function checkpwd() {
    if ($("#pwd").val() == "") {
        $("#pwdTip").html("*密码不能为空");
        return false;
    }
    if (!regs.test($("#pwd").val())) {
        $("#pwdTip").html(errormsg);
        return false;
    }
    else {
        $("#pwdTip").hide();
        return true;
    }
}

function comfirmcheck() {
    if ($("#pwdComfirm").val() == "") {
        $("#comTip").html("*确认密码不能为空");
        return false;
    }
    if ($("#pwd").val() != $("#pwdComfirm").val()) {
        $("#comTip").html("*两次密码输入不相同");
        return false
    }
    else { $("#comTip").hide(); return true; }
}

//验证邮箱
function CheckEmail(obj) {
    var status;
    var regs = /^[\w-]+(\.[\w-]+)*\@[A-Za-z0-9]+((\.|-|_)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/;
    var emailval = obj.val();
    if (emailval != "") {
        if (!regs.test(emailval)) {
            $("#emailTip").html("<span style=\"color:red\">请填写有效的Email地址</span>");
            status = false;
        } else {
            //验证注册邮箱是否存在
            $("#emailTip").hide();
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
                        $("#emailTip").hide();
                        status = true;
                    }
                    else {
                        $("#emailTip").show().html("<span style=\"color:red\">该Email已存在，请使用其他Email地址。使用该地址<a href='/Account/Login'>登录</a>，忘记密码请点击<a href='/Account/FindPwd' >找回密码</a></span>");
                        status = false;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                    status = false;
                }

            });
        }
    } else {
        $("#emailTip").html("<span style=\"color:red\">请填写有效的Email地址</span>");
        status = false;
    }
    return status;
}

//验证协议
function CheckAgreement(obj) {
    if (obj.attr("checked")) {
        $("#divAgreementTip").removeClass("msg msg-err").removeClass("msg msg-info").html("");
        agreementStatus = true;
    } else {
        $("#divAgreementTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请先阅读并同意《用户服务协议》</p>");
        agreementStatus = false;
    }
}
