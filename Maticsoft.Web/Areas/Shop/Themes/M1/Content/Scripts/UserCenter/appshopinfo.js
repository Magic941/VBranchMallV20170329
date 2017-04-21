
/**
* updateuserinfo.js
*
* 功 能：修改用户信息
* 文件名称： updateuserinfo.js
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/06/18 12:00:00      初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/


/*错误警告提示信息*/
var warningTip = "<p class=\"noticeWrap\"> <b class=\"ico-warning\"></b><span class=\"txt-err\">{0}</span>";
/*成功的提示信息*/
var succTip = "<p class=\"noticeWrap\"><b class=\"ico-succ\"></b><span class=\"txt-succ\">{0}</span>";
/*鼠标移上去*/
var mouseonTip = "<div class=\"txt-info-mouseon\"  style=\"display:none;\">{0}</div>";
/* 鼠标离开*/
var mouseoutTip = "<div class=\"txt-info-mouseout\"  style=\"display:none;\">{0}</div>";


$(function () {

    /*验证用户昵称开始*/
    $("#txtNickName").focus(function () {
        $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>填写昵称");
    }).blur(function () {
        checknickname();

    });
    /*验证用户结束*/

    $("#txtUserName").focus(function () {
        $("#usernameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>填写用户名");
    }).blur(function () {
            checkUserName();
    })

    /*验证邮箱开始*/
    $("#txtEmail").focus(function () {
        $("#emailTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>填写邮箱");
    }).blur(function () {
        checkemail();
    });
    /*验证邮箱结束*/

    /*验证手机号码开始*/
    $("#txtPhone").blur(function () {
        checkphone();
    });
    /*验证手机号码结束*/

    /*验证固定电话开始*/
    $("#txtTelPhone").blur(function () {
        checktelphone();
    });
    /*验证固定电话结束*/

    /*验证所在地开始*/
    $("#txtAddress").blur(function () {
        checkaddress();
    });
    /*验证所在地结束*/

    /*验证生日开始*/
    $("#txtBirthday").blur(function () {
        checkbirthday();
    });
    /*验证生日结束*/

    /*验证个人标签开始*/
    $("#txtRemark").blur(function () {
        checkremark();
    });
    /*验证个人标签结束*/

    /*验证用户自我介绍开始*/
    $("#txtSingature").blur(function () {
        checksingature();
    });
    /*验证用户自我介绍结束*/



});

function checkUserName() {
    var result = true;
    var username = $("#txtUserName").val();
    if (username != $("#h_username").val()) {
        if (!(/^[a-zA-Z0-9_]{3,10}$/).test(username)) {
            $("#usernameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>用户名必须为3-14数字、字母、下划线组合！");
            result = false;
        }
        else {
            $.ajax({
                url: '/Account/ExistsUserName',
                type: "post",
                async: false,
                data: { "UserName": username },
                success: function (data) {
                    if (data == "True") {
                        $("#usernameTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
                        result = true;
                    }
                    else {
                        $("#usernameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>用户名已存在，请重新填写！");
                        result = false;
                    }
                }
            })

        }
    }
    else {
        $("#usernameTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
    }
    return result;
}

// 验证用户昵称
function checknickname() {
    var i = 0;
    var nicknameVal = $.trim($('#txtNickName').val());
    if (nicknameVal.indexOf(";") > -1 || nicknameVal.indexOf(",") > -1 || nicknameVal.indexOf("'") > -1) {
        ShowFailTip('大神，请您手下留情！');
        $(this).val("");
        i++;
        if (i >= 3) {
            ShowFailTip('别玩了，这样有意思吗？');
        }
        return false;
    }
    if (nicknameVal != "") {
        //验证昵称是否存在
        var errnum = 0;
        $.ajax({
            url: $Maticsoft.BasePath + "UserCenter/CheckNickName",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                NickName: nicknameVal
            },
            success: function (JsonData) {
                switch (JsonData.STATUS) {
                    case "OK":
                        $("#nciknameTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
                        break;
                    case "EXISTS":
                        errnum++;
                        $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>该昵称已被其他用户抢先使用，换一个试试");
                        break;
                    case "NOTNULL":
                        errnum++;
                        $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>昵称不能为空！");
                        break;
                    default:
                        errnum++;
                        ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                        break;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                errnum++;
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
        return errnum == 0 ? true : false;
    } else {
        $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>昵称不能为空！");
        return false;
    }
}

// 验证用户性别
function checksex() {
    var sex = 2;
    if ($("#radman").attr("checked") == "checked") {
        sex = 1;
    }
    if ($("#radwoman").attr("checked") == "checked") {
        sex = 0;
    }
    if (sex == 2) {
        $("#sexTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>请选择您的性别！");
        return false;
    } else {
        $("#sexTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
        return true;
    }
}

// 验证用户邮箱
function checkemail() {
    /*验证用户邮箱开始*/
    var emailVal = $.trim($('#txtEmail').val());
    if (emailVal == '') {
        $("#emailTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i>邮箱不能为空！");
        return false;
    } else if (emailVal.length > 100) {
        $("#emailTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>邮箱长度不能超过100个字符！");
        return false;
    } else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test(emailVal)) {
        $("#emailTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>请填写有效的Email地址");
        return false;
    } else {
        $("#emailTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
        return true;
    }
    /*验证用户邮箱结束*/
}
// 验证用户手机号码
function checkphone() {
    var phoneVal = $.trim($('#txtPhone').val());
    if (phoneVal != '') {
        if (!/^(1[3,4,5,8,7]{1}\d{9})$/.test(phoneVal)) {
            $("#phoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>请输入正确的手机号！");
            return false;
        } else {
            $("#phoneTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
            return true;
        }
    }
    return true;
}

// 验证用户固定电话
function checktelphone() {
    var telphoneVal = $.trim($('#txtTelPhone').val());
    if (telphoneVal != '') {
        //if (!/^(0[0-9]{2,3}-)?([2-9][0-9]{6,7})+(-[0-9]{1,4})?$/.test(telphoneVal)) {
        var tel = /(^[0-9]{3,4}\-[0-9]{7,8}$)|(^[0-9]{7,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}13[0-9]{9}$)|(13\d{9}$)|(15[0135-9]\d{8}$)|(18[267]\d{8}$)/;
        if (!tel.test(telphoneVal)) {
            $("#telphoneTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>请输入正确的电话号码！");
            return false;
        } else {
            $("#telphoneTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
            return true;
        }
    }
    return true;
}

// 验证用户所在地
function checkaddress() {
    var addressVal = $.trim($('#txtAddress').val());
    if (addressVal != '') {
        if (addressVal.length > 100) {
            $("#addressTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>请控制在0-300字符内！");
            return false;
        }
        $("#addressTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
    }
    return true;
}

//短日期，形如 (2008-08-08)
function isDate(str) {
    var r = str.match(/^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/);
    if (r == null) return false;
    var d = new Date(r[1], r[3] - 1, r[4]);
    return (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4]);
}

// 验证用户生日
function checkbirthday() {

    var birthdayVal = $.trim($('#txtBirthday').val());
    if (birthdayVal != '') {
        if (!isDate(birthdayVal)) {
            $("#birthdayTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>生日格式错误！");
            return false;
        }
        $("#birthdayTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
    }
    return true;

}


// 验证用户个人标签
function checkremark() {
    var remarkVal = $.trim($('#txtRemark').val());
    if (remarkVal != '') {
        if (remarkVal.length > 100) {
            $("#remarkTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>请控制在0~100字符！");
            return false;
        }
        $("#remarkTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");
    }

    return true;
}

// 验证用户自我介绍
function checksingature() {
    var singatureVal = $.trim($('#txtSingature').val());
    if (singatureVal != '') {
        if (singatureVal.length > 200) {
            $("#singatureTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i>请控制内容在0~200字符！");
            return false;
        }
        $("#singatureTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i>&nbsp;");

    }
    return true;
}

function submit() {

    var errnum = 0;
    if (!checkUserName()) {
        errnum++;
    }
    if (!checknickname()) {
        errnum++;
    }

    if (!checkemail()) {
        errnum++;
    }
    //if (!checksex()) {
    //    errnum++;
    //}
    if (!checkphone()) {
        errnum++;
    }

    if (!checktelphone()) {
        errnum++;
    }
    if (!checkaddress()) {
        errnum++;
    }

    if (!checkbirthday()) {
        errnum++;
    }

    if (!checkremark()) {
        errnum++;
    }
    if (!checksingature()) {
        errnum++;
    }
    if (!(errnum == 0 ? true : false)) {
        return false;
    } else {
        var username = $.trim($("#txtUserName").val())
        var nickname = $.trim($("#txtNickName").val());
        var email = $.trim($("#txtEmail").val());
        var phone = $.trim($("#txtPhone").val());
        var telphone = $.trim($("#txtTelPhone").val());
        var birthday = $.trim($("#txtBirthday").val());
        var address = $.trim($("#hfSelectedNode").val());
        var remark = $.trim($("#txtRemark").val());
        var singature = $.trim($("#txtSingature").val());
        var constellation = $.trim($("#dropConstellation").val());
        var personalstatus = $.trim($("#dropPersonalStatus").val());
        var sex = -1;
        if ($("#radman").attr("checked") == "checked") {
            sex = 1;
        }
        if ($("#radwoman").attr("checked") == "checked") {
            sex = 0;
        }
        $.ajax({
            url: $Maticsoft.BasePath + "UserCenter/UpdateUserInfo",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                UserName: username,
                NickName: nickname,
                Email: email,
                Phone: phone,
                TelPhone: telphone,
                Birthday: birthday,
                Address: address,
                Remark: remark,
                Singature: singature,
                Constellation: constellation,
                PersonalStatus: personalstatus,
                Sex: sex
            },
            success: function (JsonData) {
                switch (JsonData.STATUS) {
                    case "SUCC":
                        ShowSuccessTip("修改成功！");
                        $(".ic_toolbar", document.body).load("/Partial/Header");
                        setTimeout(function () { location = "/UserCenter/Personal"; }, 500);
                        break;
                    case "FAIL":
                        ShowFailTip("修改失败！");
                        break;
                    default:
                        ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                        break;
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }

        });
    }

};
