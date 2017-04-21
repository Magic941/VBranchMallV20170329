/*
* File:        maticsoft.InfoBind.js
* Author:      tush@maticsoft.com, qihq@maticsoft.com
* Copyright © 2012 Maticsoft. All Rights Reserved.
*/



$(function () {

    if (parseInt($("#bindcount").val())==0) {
        $(".isSendAll").attr("disabled", "disabled");
    }
    $(".isSendAll").die("click").live("click", function () {
        if ($(this).attr('checked') != undefined) {
            $(this).parent("p").find(".weibo").removeClass("g_sina").addClass("i_sina").attr("title", "同步到新浪微博"); ; ;
            if ($(this).parent("p").find(".g_qzone").attr("value") != "") {
                $(this).parent("p").find(".g_qzone").removeClass("grey").attr("title", "同步到新浪微博"); ;
            }
        } else {
            $(this).parent("p").find(".weibo").removeClass("i_sina").addClass("g_sina").attr("title", "同步到新浪微博"); ;
            $(this).parent("p").find(".g_qzone").addClass("grey").attr("title", "同步到新浪微博"); ;

        }

    });
    $(".weibo").die("click").live("click", function () {
        if ($(this).attr("s_type") == "2") {
            $(this).removeClass("g_sina").addClass("i_sina");
            $(this).attr("s_type", "1");
            $(this).attr("title", "已绑定到新浪微博");
        }
        else {
            $(this).removeClass("i_sina").addClass("g_sina");
            $(this).attr("s_type", "2");
            $(this).attr("title", "未绑定到新浪微博");
        }
    });
    $(".close5").click(function () {
        $(".tanchu,.btmbg2").fadeOut();
        $("body").css("overflow-y", "auto");
    });
    $(".g_qzone").die("click").live("click", function () {
        if ($(this).attr("s_type") == "2" && $(this).attr("value") != "") {
            $(this).removeClass("grey");
            $(this).attr("s_type", "1");
            $(this).attr("title", "已绑定到腾讯微博");
        }
        else {
            $(this).addClass("grey");
            $(this).attr("s_type", "2");
            $(this).attr("title", "未绑定到新浪微博");
        }
    });


});

function CancelBind(Id) {
    $.ajax({
        url: $Maticsoft.BasePath + "UserCenter/CancelBind",
        type: 'post',
        data: { Action: "post", BindId: Id },
        dataType: 'text',
        success: function (resultData) {
            if (resultData == "success") {
                window.location = $Maticsoft.BasePath + "UserCenter/UserBind";
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("请求错误！");
        }
    });
}
function btnSet(control) {
    alert($(control).attr("checked"));
}
function btnBind(text) {
    $.jBox.info("还没有绑定" + text + "帐号，点击这里<a href='/UserCenter/UserBind'  tyle='color: #fff;' target='_blank'>开始绑定</a>", "帐号绑定", { width: 350, top: 400 });
    $(".jbox-button").hide();
    //        $.jBox.confirm("还没有绑定" + text + "帐号，点击这里<a href='/UserCenter/UserBind'  tyle='color: #fff;' target='_blank'>开始绑定</a>", "帐号绑定", submit, { width: 350, top: 400 });
}
(function () {
    var CurrentOption;
    function InfoSending(option) {
        CurrentOption = option;
        if (CurrentOption.mediaIds != "") {
            $.ajax({
                url: $Maticsoft.BasePath + "profile/AjaxPostWeibo",
                type: 'post', dataType: 'text', timeout: 10000,
                data: { Type: CurrentOption.Type, MediaIds: CurrentOption.mediaIds, ShareDes: CurrentOption.ShareDes, ImageUrl: CurrentOption.ImageUrl, TargetID: CurrentOption.TargetID, PostId: CurrentOption.PostID, TopicID: CurrentOption.TopicID, AlumbID: CurrentOption.AlumbID,ReplyId:CurrentOption.ReplyId , VideoRawUrl: CurrentOption.VideoRawUrl },
                success: function (resultData) {
                    $(".bind>span").attr("s_type", "2");
                    $(".weibo").removeClass("i_sina").addClass("g_sina");
                    $(".g_qzone").addClass("grey");
                    $('.isSendAll').prop('checked', false);
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
               
                }
            });
        }
    }
    window['InfoSync'] = {};
    window['InfoSync']['InfoSending'] = InfoSending;

} ());

