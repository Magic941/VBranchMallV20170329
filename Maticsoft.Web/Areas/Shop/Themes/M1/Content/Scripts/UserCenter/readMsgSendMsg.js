/**
* $itemname$.js
*
* 功 能： [N/A]
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  $time$  $username$    初版
*
* Copyright (c) $year$ Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

$(function() {
    //点击删除触发
    $(".DelReceiveMsg").click(function() {
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: "/UserCenter/DelReceiveMsg",
            data: { MsgID: $(this).attr("itemid") },
            success: function(data) {
                if (data.STATUS == "SUCC") {
                    ShowSuccessTip('删除成功');
                    setTimeout(function () {
                        location.href = $Maticsoft.BasePath + "UserCenter/InboxList";
                    }, 2000);
                } else {
                    ShowFailTip('出现异常');
                }
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
    });



     
    /*验证主题开始*/
    $("#txtTitle").focus(function () {
        $("#titleTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写主题</p>");
    }).blur(function () {

        checktitle();

    });
    /*验证主题结束*/

    /*验证内容开始*/
    $("#txtContent").focus(function () {
        $("#contentTip").removeClass("msg msg-ok msg-naked").removeClass("msg msg-err").addClass("msg msg-info").html("<i class=\"msg-ico\"></i><p>填写内容</p>");
    }).blur(function () {
        checkcontent();
    });
    /*验证内容结束*/
});


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

//发送消息
function submitSendMsg() {
    if (!checktitle()) {
        return false;
    }

    if (!checkcontent()) {
        return false;
    }
    var receiverID = $.trim($("#hidreceiverid").val());
        var title = $.trim($("#txtTitle").val());
        var content = $.trim($("#txtContent").val());
    var error = 0;
    $.ajax({
        type: "POST",
        dataType: 'json',
        url: "/UserCenter/ReplyMsg",
        data: { ReceiverID: receiverID, Title: title, Content: content },
        success: function (data) {
            if (data.STATUS == "SUCC") {
                error = 1;
                 $("#txtTitle").val('') ;
                 $("#txtContent").val('') ;
                ShowSuccessTip('发送成功');
            } else {
                ShowFailTip('出现异常');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
        }
    });
    return error == 1 ? true : false;
}