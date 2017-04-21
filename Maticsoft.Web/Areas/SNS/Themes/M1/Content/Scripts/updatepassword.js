/**
* updatepassword.js
*
* 功 能：修改密码
* 文件名称： updatepassword.js
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/09/25 12:00:00  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

/*错误警告提示信息*/
var warningTip = "<p class=\"noticeWrap\"> <b class=\"ico-warning\"></b><span class=\"txt-err\">{0}</span></p>";
/*成功的提示信息*/
var succTip = "<p class=\"noticeWrap\"><b class=\"ico-succ\"></b><span class=\"txt-succ\">{0}</span></p>";
/*鼠标移上去*/
var mouseonTip = "<div class=\"txt-info-mouseon\"  style=\"display:none;\">{0}</div>";
/* 鼠标离开*/
var mouseoutTip = "<div class=\"txt-info-mouseout\"  style=\"display:none;\">{0}</div>";

function addcss(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt");
}

function addclass(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipts");
}

function addactivecss(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt-active");
}

function adderrcss(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt-err");
}

function adderrclass(controlId) {
    $("#" + controlId).removeClass();
    $("#" + controlId).addClass("g-ipt-errs");
}

function cleartip() {
    $("#pwdTip").empty();
    $("#newpwdTip").empty();
    $("#confirmpwdTip").empty();
}

function Initializationstyle() {
    addcss("txtPwd");
    addcss("txtNewPwd");
    addclass("txtConfirmPwd2");
}

$(function () {

    /*密码开始*/
    $("#txtPwd").focus(function () {
        $("#pwdTip").empty();
        addactivecss("txtPwd");
    });

    $("#txtPwd").blur(function () {

        checkpassword();

    });
    /*密码结束*/

    /*新密码开始*/
    $("#txtNewPwd").focus(function () {
        $("#newpwdTip").empty();
        addactivecss("txtNewPwd");
    });

    $("#txtNewPwd").blur(function () {

        checknewpassword();

    });
    /*新密码开始*/

    /*确认密码开始*/
    $("#txtConfirmPwd2").focus(function () {

        addactivecss("txtConfirmPwd2");

    });

    $("#txtConfirmPwd2").blur(function () {
        addcss("txtConfirmPwd2");
        checkconfirmpassword();

    });
    /*确认密码结束*/

});

// 验证用户原密码
function checkpassword() {

    var errnum = 0;

    var passwordVal = $.trim($('#txtPwd').val());

    if (passwordVal == '') {
        adderrcss("txtPwd");
        $("#pwdTip").empty();
        $("#pwdTip").append(warningTip.format("原密码不能为空！"));
        return false;
    } else {
        $.ajax({
            url: $Maticsoft.BasePath + "UserCenter/CheckPassword",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                Password: passwordVal
            },
            success: function(JsonData) {

                if (JsonData.STATUS == "ERROR") {
                    errnum++;
                    adderrcss("txtPwd");
                    $("#pwdTip").empty();
                    $("#pwdTip").append(warningTip.format("原密码错误！"));
                } else if (JsonData.STATUS == "OK") {
                    addcss("txtPwd");
                    $("#pwdTip").empty();
                    $("#pwdTip").append(succTip.format("原密码输入正确！"));
                } else {
                    errnum++;
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                }
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                errnum++;
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
    }
    return errnum == 0 ? true: false;
}

// 验证用户新密码
function checknewpassword() {

    var newpasswordVal = $.trim($('#txtNewPwd').val());

    if (newpasswordVal == '') {
        adderrcss("txtNewPwd");
        $("#newpwdTip").empty();
        $("#newpwdTip").append(warningTip.format("新密码不能为空！"));
        return false;
    }
    else if (newpasswordVal.length < 6 || newpasswordVal.length > 16) {
        adderrcss("txtNewPwd");
        $("#newpwdTip").empty();
        $("#newpwdTip").append(warningTip.format("新密码长度为6~16个字符！"));
        return false;
    }
    else {
        addcss("txtNewPwd");
        $("#newpwdTip").empty();
        $("#newpwdTip").append(succTip.format("新密码输入正确！"));
        return true;
    }
}

// 验证用户确认密码
function checkconfirmpassword() {

    var newpasswordVal = $.trim($('#txtNewPwd').val());
    var confirmpwdVal = $.trim($('#txtConfirmPwd2').val());
    if (newpasswordVal == '') {
        adderrcss("txtNewPwd");
        $("#newpwdTip").empty();
        $("#newpwdTip").append(warningTip.format("新密码不能为空！"));
        return false;
    }
    else if (newpasswordVal.length < 6 || newpasswordVal.length > 16) {
        adderrcss("txtNewPwd");
        $("#newpwdTip").empty();
        $("#newpwdTip").append(warningTip.format("新密码长度为6~16个字符！"));
        return false;
    }
    else if (confirmpwdVal == '') {
        adderrcss("txtConfirmPwd2");
        $("#confirmpwdTip").empty();
        $("#confirmpwdTip").append(warningTip.format("确认密码不能为空！"));
        return false;
    }
    else if (newpasswordVal != confirmpwdVal) {
        adderrcss("txtConfirmPwd2");
        $("#confirmpwdTip").empty();
        $("#confirmpwdTip").append(warningTip.format("两次密码不一致,请确认！"));
        return false;
    }
    addcss("txtConfirmPwd2");
    $("#confirmpwdTip").empty();
    $("#confirmpwdTip").append(succTip.format("确认密码输入正确！"));
    return true;

}

function submit() {

    var errnum = 0;

    if (!checkpassword()) {
        errnum++;
    }

    if (!checknewpassword()) {
        errnum++;
    }
    if (!checkconfirmpassword()) {
        errnum++;
    }

    if (!(errnum == 0 ? true : false)) {
        return false;
    } else {

        var newpasswordVal = $.trim($('#txtNewPwd').val());
        var confirmpwdVal = $.trim($('#txtConfirmPwd2').val());
        $.ajax({
            url: $Maticsoft.BasePath + "UserCenter/UpdateUserPassword",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                NewPassword: newpasswordVal,
                ConfirmPassword: confirmpwdVal
            },
            success: function (JsonData) {
                switch (JsonData.STATUS) {
                    case "FAIL":
                        ShowServerBusyTip("新密码和确认密码不一致！");
                        break;
                    case "UPDATESUCC":
                        $("#txtPwd").val("");
                        $("#txtNewPwd").val("");
                        $("#txtConfirmPwd2").val("");
                        $("#pwdTip").empty();
                        $("#newpwdTip").empty();
                        $("#confirmpwdTip").empty();
                        ShowSuccessTip("修改密码成功！");
                        break;
                    case "UPDATEFAIL":
                        ShowFailTip("修改密码失败！");
                        break;
                    default:
                        ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                        break;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！")
            }
        });

    }

}