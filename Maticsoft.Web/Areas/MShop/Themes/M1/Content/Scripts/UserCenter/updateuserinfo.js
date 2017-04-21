
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
var warningTip = "<p class=\"noticeWrap\"> <b class=\"ico-warning\"></b><span class=\"txt-err\">{0}</span></p>";
/*成功的提示信息*/
var succTip = "<p class=\"noticeWrap\"><b class=\"ico-succ\"></b><span class=\"txt-succ\">{0}</span></p>";
/*鼠标移上去*/
var mouseonTip = "<div class=\"txt-info-mouseon\"  style=\"display:none;\">{0}</div>";
/* 鼠标离开*/
var mouseoutTip = "<div class=\"txt-info-mouseout\"  style=\"display:none;\">{0}</div>";


$(function () {

    /*验证用户昵称开始*/
    $("#txtNickName").focus(function () {
        $("#nciknameTip").removeClass("red").addClass("tipClass").html("填写昵称");
    }).blur(function () {
        checknickname();
    });
    /*验证用户结束*/

    /*验证真实姓名开始*/
    $("#txtTrueName").focus(function () {
        $("#TrueNameTip").removeClass("red").addClass("tipClass").html("填写真实姓名");
    }).blur(function () {
        checkTrueName();
    });
    /*验证用户结束*/


    /*验证邮箱开始*/
    $("#txtEmail").focus(function () {
        $("#emailTip").removeClass("red").addClass("tipClass").html("填写邮箱");
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

// 验证用户所在地
function checkTrueName() {
    var truenameVal = $.trim($('#txtTrueName').val());
    if (truenameVal != '') {
        if (truenameVal.length > 20) {
            $("#TrueNameTip").removeClass("tipClass").addClass("red").html("请控制在0-20字符内！");
            return false;
        }
        $("#TrueNameTip").removeClass("red").addClass("tipClass").html("&nbsp;");
    }
    return true;
}

// 验证用户昵称
function checknickname() {
    var i = 0;
    var nicknameVal = $.trim($('#txtNickName').val());
    if (nicknameVal.indexOf(";") > -1 ) {
        $("#nciknameTip").removeClass("tipClass").addClass("red").html("昵称不能包含“；”");
        $(this).val("");
        i++;
        if (i >= 3) {
            $("#nciknameTip").removeClass("tipClass").addClass("red").html('别玩了，这样有意思吗？');
        }
        return false;
    }
    if (nicknameVal != "") {
        //验证昵称是否存在
        var errnum = 0;
        $.ajax({
            url: $Maticsoft.BasePath +"u/CheckNickName" ,
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
                        $("#nciknameTip").removeClass("red").addClass("tipClass").html("&nbsp;");
                        break;
                    case "EXISTS":
                        errnum++;
                        $("#nciknameTip").removeClass("tipClass").addClass("red").html("该昵称已被其他用户抢先使用，换一个试试");
                        break;
                    case "NOTNULL":
                        errnum++;
                        $("#nciknameTip").removeClass("tipClass").addClass("red").html("昵称不能为空！");
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
        $("#nciknameTip").removeClass("tipClass").addClass("red").html("昵称不能为空！");
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
        $("#sexTip").removeClass("tipClass").addClass("red").html("请选择您的性别！");
        return false;
    } else {
        $("#sexTip").removeClass("red").addClass("tipClass").html("&nbsp;");
        return true;
    }
}

// 验证用户邮箱
function checkemail() {
    /*验证用户邮箱开始*/
    var emailVal = $.trim($('#txtEmail').val());
    if (emailVal == '') {
        $("#emailTip").removeClass("tipClass").addClass("red").html("邮箱不能为空！");
        return false;
    } else if (emailVal.length > 100) {
        $("#emailTip").removeClass("tipClass").addClass("red").html("邮箱长度不能超过100个字符！");
        return false;
    } else if (!/^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*/.test(emailVal)) {
        $("#emailTip").removeClass("tipClass").addClass("red").html("请填写有效的Email地址");
        return false;
    } else {
        $("#emailTip").removeClass("red").addClass("tipClass").html("&nbsp;");
        return true;
    }
    /*验证用户邮箱结束*/
}
// 验证用户手机号码
function checkphone() {
    var phoneVal = $.trim($('#txtPhone').val());
    if (phoneVal != '') {
        if (!/^(1[3,4,5,8,7]{1}\d{9})$/.test(phoneVal)) {
            $("#phoneTip").removeClass("tipClass").addClass("red").html("请输入正确的手机号！");
            return false;
        } else {
            $("#phoneTip").removeClass("red").addClass("tipClass").html("&nbsp;");
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
        if(!tel.test(telphoneVal)){
            $("#telphoneTip").removeClass("tipClass").addClass("red").html("请输入正确的电话号码！");
            return false;
        } else {
            $("#telphoneTip").removeClass("red").addClass("tipClass").html("&nbsp;");
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
            $("#addressTip").removeClass("tipClass").addClass("red").html("请控制在0-300字符内！");
            return false;
        }
        $("#addressTip").removeClass("red").addClass("tipClass").html("&nbsp;");
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
            $("#birthdayTip").removeClass("tipClass").addClass("red").html("生日格式错误！");
            return false;
        }
        $("#birthdayTip").removeClass("red").addClass("tipClass").html("&nbsp;");
    }    
    return true;

}


// 验证用户个人标签
function checkremark() {
    var remarkVal = $.trim($('#txtRemark').val());
    if (remarkVal != '') {
        if (remarkVal.length > 100) {
            $("#remarkTip").removeClass("tipClass").addClass("red").html("请控制在0~100字符！");
            return false;
        }
        $("#remarkTip").removeClass("red").addClass("tipClass").html("&nbsp;");
    }
   
    return true;
}

// 验证用户自我介绍
function checksingature() {
    var singatureVal = $.trim($('#txtSingature').val());
    if (singatureVal != '') {
        if (singatureVal.length > 200) {
            $("#singatureTip").removeClass("tipClass").addClass("red").html("请控制内容在0~200字符！");
            return false;
        }
        $("#singatureTip").removeClass("red").addClass("tipClass").html("&nbsp;");
 
    }
      return true;
}

function submit() {

    var errnum = 0;
    if (!checknickname()) {
        errnum++;
    }
    if (!checkTrueName()) {
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
        var truename = $.trim($("#txtTrueName").val());
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
            url: $Maticsoft.BasePath +"u/UpdateUserInfo",
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
                Sex: sex,
                TrueName:truename
            },
            success: function (JsonData) {

                switch (JsonData.STATUS) {
                    case "SUCC":
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
