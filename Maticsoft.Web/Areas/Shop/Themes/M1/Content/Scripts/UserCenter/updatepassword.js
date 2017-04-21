/**
* updatepassword.js
*
* 功 能：修改密码
* 文件名称： updatepassword.js
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/06/18 12:00:00   HUHY  初版
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

 

$(function () {
    /*密码开始*/
    $("#txtPwd").focus(function () {
        $("#pwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>请填写原密码</p>");
    }).blur(function () {
        checkpassword();
    });
    /*密码结束*/

    /*新密码开始*/
    $("#txtNewPwd").focus(function () {
        $("#newpwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写新密码</p>");
    }).blur(function () {
        checknewpassword();
    });
    /*新密码开始*/

    /*确认密码开始*/
    $("#txtConfirmPwd").focus(function () {
        $("#confirmpwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>确认密码</p>");
    }).blur(function () {
        
        checkconfirmpassword();

    });
    /*确认密码结束*/

});

// 验证用户原密码
function checkpassword() {

    var errnum = 0;

    var passwordVal = $.trim($('#txtPwd').val());

    if (passwordVal == '') {
        $("#pwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>原密码不能为空！</p>");
        return false;
    } else {
        $.ajax({
            url:   $Maticsoft.BasePath + 'UserCenter/CheckPassword' ,
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
                    $("#pwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>原密码错误！</p>");
                } else if (JsonData.STATUS == "OK") {
                    $("#pwdTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
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
        $("#newpwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>新密码不能为空！</p>");
        return false;
    }
    else if (newpasswordVal.length < 6 || newpasswordVal.length > 16) {
        $("#newpwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>新密码长度为6~16个字符！</p>");
        return false;
    }
    else {
        $("#newpwdTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
        return true;
    }
}

// 验证用户确认密码
function checkconfirmpassword() {

    var newpasswordVal = $.trim($('#txtNewPwd').val());
    var confirmpwdVal = $.trim($('#txtConfirmPwd').val());   
    if (confirmpwdVal == '') {
        $("#confirmpwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>确认密码不能为空！</p>");
        return false;
    }
    else if (newpasswordVal != confirmpwdVal) {
        $("#confirmpwdTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>两次密码不一致,请确认！</p>");
        return false;
    }
    $("#confirmpwdTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
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
        var confirmpwdVal = $.trim($('#txtConfirmPwd').val());
        $.ajax({
            url:  $Maticsoft.BasePath +'UserCenter/UpdateUserPassword' ,
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
                        $("#txtConfirmPwd").val("");
                        $("#pwdTip").removeClass("msg msg-ok msg-naked");
                        $("#newpwdTip").removeClass("msg msg-ok msg-naked");
                        $("#confirmpwdTip").removeClass("msg msg-ok msg-naked");
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
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });

    }
}