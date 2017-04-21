/*
* File:        maticsoft.jquery.sms.js
* Author:      yaoy@maticsoft.com
* Copyright © 2004-2012 Maticsoft. All Rights Reserved.
*/
;
function CheckPhoneAndSendSMS(smsFun) {
    $("#sendCode").attr("disabled", "disabled");
    $("#sendCode").attr("value", "正在验证请稍后");
    //            $("[id$='txtPhone']").attr("disabled", "disabled");

    var reg = /^[1][3|5|8][0-9]\d{8}$/;
    if (!reg.exec($("[id$='txtPhone']").val())) {
        $("#errorMSG").html("<span>输入有效的手机号码</span>");
        $("#errorMSG").show();
        $("#sendCode").attr("disabled", "disabled");
        $("#sendCode").attr("value", "请输入正确的手机号码");

        //                $("[id$='txtPhone']").removeAttr("disabled");
    } else {
        $.ajax({
            url: "RegionHandle.aspx",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            async: false, // 同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行。
            data: { action: "CheckPhone", PhoneNumber: $("[id$='txtPhone']").val() },
            success: function (resultData) {
                if (resultData == "yes") {
                    $("#sendCode").attr("disabled", "disabled");
                    $("#errorMSG").html("<span>此电话已存在!</span>");
                    $("#errorMSG").show();
                    $("#sendCode").attr("disabled", "disabled");
                    $("#sendCode").attr("value", "请输入正确的手机号码");
                }
                else {
                    $("#errorMSG").html("<span>电话号码有效</span>");
                    $("#errorMSG").show();
                    if (isOK) {
                        $("#sendCode").removeAttr("disabled");
                        $("#sendCode").attr("value", "点击发送验证码");
                    }
                }
                if (smsFun != undefined) {
                    smsFun();
                }
                //                        $("[id$='txtPhone']").removeAttr("disabled");
            },

            error: function (xmlHttpRequest, textStatus, errorThrown) {
                $.alertError(xmlHttpRequest.responseText);
            }
        });
    }
}

function CheckCode() {
    if ($("[id$='txtCode']").val().length == 6) {
        $("#errorCode").hide();
        $("[id$='btnNext']").removeAttr("disabled");
        return true;
    } else {
        $("#errorCode").show();
        $("[id$='btnNext']").attr("disabled", "disabled");
        return false;
    }
}

var isOK = true;
var smsSeconds = 180;
var intervaSMS;

function CountDown() {
    if (smsSeconds < 0) {
        //                $("[id$='txtPhone']").removeAttr("disabled");
        isOK = true;
        CheckPhoneAndSendSMS();
        clearInterval(intervaSMS);
    }
    else {
        $("#sendCode").attr("value", "请在(" + smsSeconds + ")秒后从新获取验证码");
        $("#sendCode").attr("disabled", "disabled");
        //                $("[id$='txtPhone']").attr("disabled", "disabled");
        isOK = false;
        smsSeconds--;
    }
}

$(document).ready(function () {
    ; if (!parent.length) { $.LockKey(); }
    CheckPhoneAndSendSMS();
    CheckCode();
    $("[id$='txtPhone']").keyup(function () {
        if ($("[id$='txtPhone']").val().length == 11) {
            if (event.keyCode != 17) {
                CheckPhoneAndSendSMS();
            }
        }
    });
    $("[id$='txtPhone']").blur(function () {
        if ($("[id$='txtPhone']").val().length == 11) {
            CheckPhoneAndSendSMS();
        }
    });

    $("[id$='txtCode']").keyup(function () {
        if (event.keyCode != 17) {
            CheckCode();
        }
    });
    $("[id$='txtCode']").blur(function () {
        CheckCode();
    });

    $("#sendCode").click(function () {
        CheckPhoneAndSendSMS(function () {
            //                    $("[id$='txtPhone']").attr("disabled", "disabled");
            var phone = $("[id$='txtPhone']").val();
            $.ajax({
                url: "RegionHandle.aspx",
                type: 'post',
                dataType: 'text',
                timeout: 10000,
                async: false, // 同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行。
                data: { Action: "sendCode", Phone: phone },
                success: function (resultData) {
                    switch (resultData) {
                        case "OK":
                            $.alert('发送短信成功, 请您接收短信后输入验证码.');
                            //                                    $("[id$='txtPhone']").attr("disabled", "disabled");
                            smsSeconds = 180;
                            $("#sendCode").attr("value", "请在(" + smsSeconds + ")秒后重新获取验证码");
                            intervaSMS = setInterval("CountDown()", 1000);
                            break;
                        case "NO":
                            $.alert('发送短信失败, 请您稍后再试.');
                            //                                    $("[id$='txtPhone']").removeAttr("disabled");
                            break;
                        default:
                            //抛出异常消息
                            $.alertError(resultData);
                            break;
                    }
                },

                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    $.alertError(xmlHttpRequest.responseText);
                }
            });
        });
    });
});