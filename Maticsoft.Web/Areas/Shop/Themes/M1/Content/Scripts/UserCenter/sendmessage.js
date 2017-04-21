/**
* sendmessage.js
*
* 功 能：发送站内信
* 文件名称：sendmessage.js
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
var warningTip = "<p class=\"noticeWrap\"> <b class=\"ico-warning\"></b><span class=\"txt-err\" >{0}</span></p>";
/*成功的提示信息*/
var succTip = "<p class=\"noticeWrap\"><b class=\"ico-succ\"></b><span class=\"txt-succ\" style='color:#777171'>{0}</span></p>";
/*鼠标移上去*/
var mouseonTip = "<div class=\"txt-info-mouseon\"  style=\"display:none;\">{0}</div>";
/* 鼠标离开*/
var mouseoutTip = "<div class=\"txt-info-mouseout\"  style=\"display:none;\">{0}</div>";

 
$(function() {

    /*验证用户昵称开始*/
    $("#txtNickName").focus(function() {
       $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写昵称</p>");
    }).blur(function() {
        checknickname();
    });
    /*验证用户结束*/

    /*验证主题开始*/
    $("#txtTitle").focus(function() {
       $("#titleTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写主题</p>");
    }).blur(function() {

        checktitle();
       
    });
    /*验证主题结束*/

    /*验证内容开始*/
    $("#txtContent").focus(function() {
        $("#contentTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写内容</p>");
    }).blur(function() {
        checkcontent();
    });
    /*验证内容结束*/

});
// 验证用户昵称
function checknickname() {
    var nicknameVal = $.trim($('#txtNickName').val());
    if (nicknameVal == '') {
          $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>昵称不能为空！</p>");
        return false;
    }
    var errnum = 0;
    $.ajax({
        url: "/UserCenter/ExistsNickName",
        type: 'post',
        dataType: 'json',
        timeout: 10000,
        async: false,
        data: {
            Action: "post",
            NickName: nicknameVal
        },
        success: function(JsonData) {
            switch (JsonData.STATUS) {
            case "EXISTS":
                $("#nciknameTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
                break;
            case "NOTEXISTS":
                errnum++;
                $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>昵称不存在！</p>");
                break;
            case "NOTNULL":
                errnum++;
                 $("#nciknameTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>昵称不能为空！</p>");
                break;
            default:
                errnum++;
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                break;
            }
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            errnum++;
            ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
        }
    });

    return errnum == 0 ? true: false;
}

// 验证主题
function checktitle() {
    var titleVal = $.trim($('#txtTitle').val());
    if (titleVal == '') {
        $("#titleTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>主题不能为空！</p>");
        return false;
    }
    if (titleVal.length == 0 || titleVal.length > 50) {
        $("#titleTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请控制在0~50字符！</p>");
        return false;
    }
    $("#titleTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
    return true;
}

// 验证内容
function checkcontent() {
    var contentVal = $.trim($('#txtContent').val());
    if (contentVal == '') {
        $("#contentTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>内容不能为空！</p>");
        return false;
    }
    if (contentVal.length == 0 || contentVal.length > 500) {
        $("#contentTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-info").addClass("msg msg-err").html("<i class=\"msg-ico\"></i><p>请控制在1~500字符！</p>");
        return false;
    }
    $("#contentTip").removeClass("msg msg-err").removeClass("msg msg-info").addClass("msg msg-ok msg-naked").html("<i class=\"msg-ico\"></i><p>&nbsp;</p>");
    return true;
}

function submit() {

    var errnum = 0;

    if (!checknickname()) {
        errnum++;
    }

    if (!checktitle()) {
        errnum++;
    }

    if (!checkcontent()) {
        errnum++;
    }

    if (! (errnum == 0 ? true: false)) {
        return false;
    } else {
        var nickname = $.trim($("#txtNickName").val());
        var title = $.trim($("#txtTitle").val());
        var content = $.trim($("#txtContent").val());

        $.ajax({
            url: "/UserCenter/SendMsg",
            type: 'post',
            dataType: 'json',
            timeout: 10000,
            async: false,
            data: {
                Action: "post",
                NickName: nickname,
                Title: title,
                Content: content,
            },
            success: function(JsonData) {

                switch (JsonData.STATUS) {
                case "NICKNAMENULL":
                    ShowServerBusyTip("昵称不能为空！");
                    break;
                case "NICKNAMENOTEXISTS":
                    ShowServerBusyTip("昵称不存在，请重新输入！");
                    break;
                case "TITLENULL":
                    ShowServerBusyTip("主题不能为空！");
                    break;
                case "CONTENTNULL":
                    ShowServerBusyTip("内容不能为空！");
                    break;
                case "SUCC":
                    $("#nciknameTip").removeClass("msg msg-ok msg-naked");
                    $("#titleTip").removeClass("msg msg-ok msg-naked");
                    $("#contentTip").removeClass("msg msg-ok msg-naked");
                    $("#txtNickName").val("");
                    $("#txtTitle").val("");
                    $("#txtContent").val("");
                    ShowSuccessTip("发送成功！");
                    break;
                case "FAIL":
                    ShowFailTip("发送失败！");
                    break;
                default:
                    ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
                    break;
                }
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }

        });
    }

}