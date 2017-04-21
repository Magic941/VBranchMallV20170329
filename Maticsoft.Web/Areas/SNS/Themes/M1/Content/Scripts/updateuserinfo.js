
/**
* updateuserinfo.js
*
* 功 能：修改用户信息
* 文件名称： updateuserinfo.js
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
    $("#nciknameTip").empty();
    $("#sexTip").empty();
    $("#emailTip").empty();
    $("#phoneTip").empty();
    $("#telphoneTip").empty();
    $("#addressTip").empty();
    $("#birthdayTip").empty();
    $("#remarkTip").empty();
    $("#singatureTip").empty();
}

function Initializationstyle() {
    addcss("txtNcikName");
    addcss("txtEmail");
    addcss("txtPhone");
    addcss("txtTelPhone");
    addcss("txtAddress");
    addcss("txtRemark");
    addcss("txtBirthday");
    addclass("txtSingature");
}

$(function () {

    /*验证用户昵称开始*/
    $("#txtNickName").focus(function () {
        addactivecss("txtNickName");
    });

    $("#txtNickName").blur(function () {
        checknickname();
    });
    /*验证用户结束*/


    /*验证邮箱开始*/
    $("#txtEmail").focus(function () {
        addactivecss("txtEmail");
    });

    $("#txtEmail").blur(function () {

        checkemail();

    });
    /*验证邮箱结束*/

    /*验证手机号码开始*/
    $("#txtPhone").focus(function () {
        addactivecss("txtPhone");
    });

    $("#txtPhone").blur(function () {

        checkphone();

    });
    /*验证手机号码结束*/

    /*验证固定电话开始*/
    $("#txtTelPhone").focus(function () {
        addactivecss("txtTelPhone");
    });

    $("#txtTelPhone").blur(function () {

        checktelphone();

    });
    /*验证固定电话结束*/

    /*验证所在地开始*/
    $("#txtAddress").focus(function () {
        addactivecss("txtAddress");
    });

    $("#txtAddress").blur(function () {

        checkaddress();

    });
    /*验证所在地结束*/

    /*验证生日开始*/
    $("#txtBirthday").focus(function () {
        addactivecss("txtBirthday");
    });

    $("#txtBirthday").blur(function () {

        checkbirthday();

    });
    /*验证生日结束*/

    /*验证个人标签开始*/
    $("#txtRemark").focus(function () {
        addactivecss("txtRemark");
    });

    $("#txtRemark").blur(function () {

        checkremark();

    });
    /*验证个人标签结束*/

    /*验证用户自我介绍开始*/
    $("#txtSingature").focus(function () {
        addactivecss("txtSingature");
    });

    $("#txtSingature").blur(function () {

        checksingature();

    });
    /*验证用户自我介绍结束*/

});


// 验证用户昵称
function checknickname() {

    var nicknameVal = $.trim($('#txtNickName').val());

    if (nicknameVal == '' || nicknameVal.length < 2 || nicknameVal.length >12) {
        adderrcss("txtNickName");
        $("#nciknameTip").empty();
        $("#nciknameTip").append(warningTip.format("昵称不能为空，长度为2-12个字符！"));
        return false;
    }

    if (!/[^ \d]/.test(nicknameVal)) {
        adderrcss("txtNickName");
        $("#nciknameTip").empty();
        $("#nciknameTip").append(warningTip.format("昵称不能全部为数字！"));
        return false;
    }
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
                    addcss("txtNickName");
                    $("#nciknameTip").empty();
                    $("#nciknameTip").append(succTip.format(""));
                    break;
                case "EXISTS":
                    errnum++;
                    adderrcss("txtNickName");
                    $("#nciknameTip").empty();
                    $("#nciknameTip").append(warningTip.format("该昵称已被其他用户使用了！"));
                    break;
                case "NOTNULL":
                    errnum++;
                    adderrcss("txtNickName");
                    $("#nciknameTip").empty();
                    $("#nciknameTip").append(warningTip.format("昵称不能为空！"));
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
        $("#sexTip").empty();
        $("#sexTip").append(warningTip.format("请选择您的性别！"));
        return false;
    } else {
        $("#sexTip").empty();
        $("#sexTip").append(succTip.format(""));
        return true;
    }
}

// 验证用户邮箱
function checkemail() {
    /*验证用户邮箱开始*/
    var emailVal = $.trim($('#txtEmail').val());
    if (emailVal == '') {
        adderrcss("txtEmail");
        $("#emailTip").empty();
        $("#emailTip").append(warningTip.format("邮箱不能为空！"));
        return false;
    } else if (emailVal.length > 100) {
        adderrcss("txtEmail");
        $("#emailTip").empty();
        $("#emailTip").append(warningTip.format("邮箱长度不能超过100个字符！"));
        return false;
    } else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test(emailVal)) {
        adderrcss("txtEmail");
        $("#emailTip").empty();
        $("#emailTip").append(warningTip.format("请输入正确格式的邮箱！"));
        return false;
    } else {
        addcss("txtEmail");
        $("#emailTip").empty();
        $("#emailTip").append(succTip.format(""));
        return true;
    }
    /*验证用户邮箱结束*/
}

// 验证用户手机号码
function checkphone() {
    var phoneVal = $.trim($('#txtPhone').val());
    if (phoneVal !='' && !/^(1[3,4,5,8,7]{1}\d{9})$/.test(phoneVal)) {
        adderrcss("txtPhone");
        $("#phoneTip").empty();
        $("#phoneTip").append(warningTip.format("请输入正确的手机号！"));
        return false;
    } else {
        addcss("txtPhone");
        $("#phoneTip").empty();
        $("#phoneTip").append(succTip.format(""));
        return true;
    }
}

// 验证用户固定电话
function checktelphone() {
    var telphoneVal = $.trim($('#txtTelPhone').val());

    if (telphoneVal !='' && !/^(0[0-9]{2,3}-)?([2-9][0-9]{6,7})+(-[0-9]{1,4})?$/.test(telphoneVal)) {
        adderrcss("txtTelPhone");
        $("#telphoneTip").empty();
        $("#telphoneTip").append(warningTip.format("请输入正确的电话号码！"));
        return false;
    } else {
        addcss("txtTelPhone");
        $("#telphoneTip").empty();
        $("#telphoneTip").append(succTip.format(""));
        return true;
    }
}

// 验证用户所在地
function checkaddress() {
    var addressVal = $.trim($('#txtAddress').val());
    if (addressVal !='' && addressVal.length > 100) {
        adderrcss("txtAddress");
        $("#addressTip").empty();
        $("#addressTip").append(warningTip.format("请控制在0-300字符内！"));
        return false;
    }
    addcss("txtAddress");
    $("#addressTip").empty();
    $("#addressTip").append(succTip.format(""));
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

    if (birthdayVal != '' && !isDate(birthdayVal) ){
        adderrcss("txtBirthday");
        $("#birthdayTip").empty();
        $("#birthdayTip").append(warningTip.format("生日格式错误！"));
        return false;
    }
    addcss("txtBirthday");
    $("#birthdayTip").empty();
    $("#birthdayTip").append(succTip.format(""));
    return true;

}


// 验证用户个人标签
function checkremark() {
    var remarkVal = $.trim($('#txtRemark').val());
    if (remarkVal != '' && remarkVal.length > 100) {
        adderrcss("txtRemark");
        $("#remarkTip").empty();
        $("#remarkTip").append(warningTip.format("请控制在0~100字符！"));
        return false;
    }
    addcss("txtRemark");
    $("#remarkTip").empty();
    $("#remarkTip").append(succTip.format(""));
    return true;
}

// 验证用户自我介绍
function checksingature() {
    var singatureVal = $.trim($('#txtSingature').val());
    if (singatureVal != '' && singatureVal.length > 200) {
        adderrcss("txtSingature");
        $("#singatureTip").empty();
        $("#singatureTip").append(warningTip.format("请控制内容在0~200字符！"));
        return false;
    }
    addclass("txtSingature");
    $("#singatureTip").empty();
    $("#singatureTip").append(succTip.format(""));
    return true;
}

function submit() {

    var errnum = 0;

    if (!checknickname()) {
        errnum++;
    }

    if (!checkemail()) {
        errnum++;
    }
    if (!checksex()) {
        errnum++;
    }
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
       var nickname = $.trim($("#txtNickName").val());
       var email = $.trim($("#txtEmail").val());
       var phone = $.trim($("#txtPhone").val());
       var telphone = $.trim($("#txtTelPhone").val());
       var birthday = $.trim($("#txtBirthday").val());
       var address = $.trim($("#hfSelectedNode").val());
       var remark = $.trim($("#txtRemark").val());
       var singature =$.trim($("#txtSingature").val());
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
                        cleartip();
                        Initializationstyle();
                        ShowSuccessTip("修改成功！");
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
