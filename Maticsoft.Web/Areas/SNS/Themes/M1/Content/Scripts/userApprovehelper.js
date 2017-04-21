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
    $("#trueNameTip").empty();
    $("#sexTip").empty();
    $("#emailTip").empty();
    $("#phoneTip").empty();
}

function Initializationstyle() {
    addcss("txtTrueName");
    addcss("txtEmail");
}

$(function () {
    /*验证用户真实姓名开始*/
    $("#txtTrueName").focus(function () {
        addactivecss("txtTrueName");
    });

    $("#txtTrueName").blur(function () {
        checkTrueTrueName();
    });
    /*验证用户真实姓名结束*/

    /*验证用户身份证号码开始*/
    $("#txtIdCard").focus(function () {
        addactivecss("txtIdCard");
    });

    $("#txtIdCard").blur(function () {
        checkIdCard();
    });
    /*验证用户身份证号码结束*/
});

// 验证用户真实姓名
function checkTrueTrueName() {
    var truenameVal = $.trim($('#txtTrueName').val());
    if (!truenameVal) {
        adderrcss("txtTrueName");
        $("#trueNameTip").empty();
        $("#trueNameTip").append(warningTip.format("请填写真实姓名！"));
        return false;
    } else {
        $("#trueNameTip").empty();
        $("#trueNameTip").append(succTip.format(""));
        return true;
    }
}

// 验证用户身份证号码
function checkIdCard() {
    var idCardVal = $.trim($('#txtIdCard').val());
    //    if (!preg_math(idCardVal)) {
    //        adderrcss("txtIdCard");
    //        $("#idCardTip").empty();
    //        $("#idCardTip").append(warningTip.format("身份证号码有误！"));
    //        return false;
    //    } else {
    //        $("#idCardTip").empty();
    //        $("#idCardTip").append(succTip.format(""));
    //        return true;
    //    }
    if (idCardVal.length < 15 || idCardVal.length > 18) {
        adderrcss("txtIdCard");
        $("#idCardTip").empty();
        $("#idCardTip").append(warningTip.format("身份证号码有误！"));
        return false;
    } else {
        $("#idCardTip").empty();
        $("#idCardTip").append(succTip.format(""));
        return true;
    }
}

//function preg_math(value) {
//    var patrn = /^([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+@([a-zA-Z0-9]+[_|\-|\.]?)*[a-zA-Z0-9]+\.[a-zA-Z]{2,3}$/;
//    if (!patrn.exec(value)) {
//        return false
//    } else {
//        return true
//    }
//}

function checkIsHavePImage() {
    if (!$("#hiddenIdCardPreImage").val()) {
        $("#idCardPTip").empty();
        $("#idCardPTip").append(warningTip.format("请上传身份证正面照！"));
        return false;
    } else {
        $("#idCardPTip").empty();
        $("#idCardPTip").append(succTip.format(""));
        return true;
    }
}


function checkIsHaveNImage() {
    if (!$("#hiddenIdCardNeImage").val()) {
        $("#idCardFTip").empty();
        $("#idCardFTip").append(warningTip.format("请上传身份证背面照！"));
        return false;
    } else {
        $("#idCardFTip").empty();
        $("#idCardFTip").append(succTip.format(""));
        return true;
    }
}

function submit() {
    var errnum = 0;
    if (!checkTrueTrueName()) {
        errnum++;
    }
    if (!checkIdCard()) {
        errnum++;
    }
    if (!checkIsHavePImage()) {
        errnum++;
    }
    if (!checkIsHaveNImage()) {
        errnum++;
    }

    if (!(errnum == 0 ? true : false)) {
        return false;
    } else {
        var truename = $.trim($("#txtTrueName").val());
        var idCardVal = $.trim($("#txtIdCard").val());
        var hiddenIdCardPreImage = $.trim($("#hiddenIdCardPreImage").val());
        var hiddenIdCardNeImage = $.trim($("#hiddenIdCardNeImage").val());
        $.ajax({
            url: $Maticsoft.BasePath + "UserCenter/UserApprove",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                TrueName: truename,
                IdCardVal: idCardVal,
                HiddenIdCardPreImage: hiddenIdCardPreImage,
                HiddenIdCardNeImage: hiddenIdCardNeImage
            },
            success: function (JsonData) {
                switch (JsonData.STATUS) {
                    case "SUCCESS":
                        window.location = $Maticsoft.BasePath + "UserCenter/SubmitApprove";
                        break;
                    case "FAILE":
                        ShowFailTip("服务器忙，请稍后再试！");
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
}


function comsubmit() {
    $.ajax({
        url: $Maticsoft.BasePath + "UserCenter/SubmitApprove",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: {
            Action: "post"
        },
        success: function (JsonData) {
            switch (JsonData.STATUS) {
                case "SUCCESS":
                    window.location = $Maticsoft.BasePath + "UserCenter/SubmitSucc";
                    break;
                case "FAILE":
                    ShowFailTip("服务器忙，请稍后再试！");
                    window.location = $Maticsoft.BasePath + "UserCenter/UserApprove";
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