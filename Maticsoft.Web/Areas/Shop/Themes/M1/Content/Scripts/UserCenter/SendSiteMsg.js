/*  huhy   2013/06/19  */


$(function () {
    //点击回复触发
    $(".SendMsg").click(function () {
        $('#nickname').val($(this).attr("nickname"));
        $("#divSendSiteMsg").dialog(dialogOpts); //弹出‘发私信’层  
    });

    //dialog层中项的设置
    var dialogOpts = {
        title: "发私信",
        width: 500,
        height: 320,
        modal: true,
        buttons: {
            "发送": function () {
                if (submitSendMsg()) {
                    $("#divSendSiteMsg").dialog("close"); //发送成功 关闭层
                }
            },
            "取消": function () {
                //  $(this).dialog("close"); //关闭层
                $("#divSendSiteMsg").dialog("close");
            }
        }
    };
    
    //点击删除触发
    $(".DelReceiveMsg").click(function () {
        $.ajax({
            type: "POST",
            dataType: 'json',
            url: "/UserCenter/DelReceiveMsg",
            data: { MsgID: $(this).attr("itemid") },
            success: function (data) {
                if (data.STATUS == "SUCC") {
                    ShowSuccessTip('删除成功');
                    $('#InBoxList').load($Maticsoft.BasePath + 'UserCenter/InboxList?page=' + $('#hidpage').val());
                } else {
                    ShowFailTip('出现异常');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
            }
        });
    });

//    //读取消息
//    $(".readMsgContent").click(function () {
//        $.ajax({
//            type: "POST",
//            dataType: 'json',
//            url: "/UserCenter/readMsgContent",
//            data: { MsgID: $(this).attr("itemid") },
//            success: function (data) {
//                if (data.STATUS == "SUCC") {
//                    ShowSuccessTip('删除成功');
//                    $('#InBoxList').load($Maticsoft.BasePath + 'UserCenter/InboxList?page=' + $('#hidpage').val());
//                } else {
//                    ShowFailTip('出现异常');
//                }
//            },
//            error: function (XMLHttpRequest, textStatus, errorThrown) {
//                ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
//            }
//        });
//    });
//    

});

//发送消息
function submitSendMsg() {
    var nickName =$('#nickname').val();
            var title = $('#title').val();
            var content = $('#content').val();

            //验证标题
            if (title == '') {
                ShowFailTip("主题不能为空！");  
                return false;
            }
            if (title.length == 0 || title.length > 50) {
                ShowFailTip("请控制在0~50字符！");  
                return false;
            }

            //验证内容
            if (content == '') {
                ShowFailTip("内容不能为空！");
                return false;
            }
            if (content.length == 0 || content.length > 500) {
                ShowFailTip('请控制在1~500字符');
                return false;
            }
    var error = 0;
    $.ajax({
        type: "POST",
        dataType: 'json',
        url: "/UserCenter/SendMsg",
        data: { NickName: nickName, Title: title, Content: content },
        success: function (data) {
            if (data.STATUS == "SUCC") {
                error=1;
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