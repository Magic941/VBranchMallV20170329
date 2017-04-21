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

$(function () {
    if ($('#hidreceiverid').val() == '-1') {////如果是管理员发送的信息 就隐藏回复功能
        $('#divReplyMsg').css('display','none');
    }


    //点击删除触发
    $(".DelReceiveMsg").click(function () {
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: $Maticsoft.BasePath +"UserCenter/DelReceiveMsg",
            data: { MsgID: $(this).attr("itemid") },
            success: function (data) {
                if (data.STATUS == "SUCC") {
                    ShowSuccessTip('删除成功');
                    setTimeout(function () {
                        location.href = $Maticsoft.BasePath +"u/Inbox";
                    }, 2000);
                } else {
                    ShowFailTip('出现异常');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
    });


    /*验证主题开始*/
    $("#txtTitle").focus(function () {
        $("#titleTip").removeClass("red").addClass("tipClass").html("填写主题");
    }).blur(function () {

        checktitle();

    });
    /*验证主题结束*/

    /*验证内容开始*/
    $("#txtContent").focus(function () {
        $("#contentTip").removeClass("red").addClass("tipClass").html("填写内容");
    }).blur(function () {
        checkcontent();
    });
    /*验证内容结束*/
});


// 验证主题
function checktitle() {
    var titleVal = $.trim($('#txtTitle').val());
    if (titleVal == '') {
        $("#titleTip").removeClass("tipClass").addClass("red").html("主题不能为空！");
        return false;
    }
    if (titleVal.length == 0 || titleVal.length > 50) {
        $("#titleTip").removeClass("tipClass").addClass("red").html("请控制在0~50字符！");
        return false;
    }
    $("#titleTip").removeClass("tipClass").addClass("tipClass").html("&nbsp;");
    return true;
}

// 验证内容
function checkcontent() {
    var contentVal = $.trim($('#txtContent').val());
    if (contentVal == '') {
        $("#contentTip").removeClass("tipClass").addClass("red").html("内容不能为空！");
        return false;
    }
    if (contentVal.length == 0 || contentVal.length > 500) {
        $("#contentTip").removeClass("tipClass").addClass("red").html("请控制在1~500字符！");
        return false;
    }
    $("#contentTip").removeClass("red").addClass("tipClass").html("&nbsp;");
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
        url: $Maticsoft.BasePath +"UserCenter/ReplyMsg",
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