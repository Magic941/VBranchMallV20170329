$(function () {
    $('#slides').slides({
        preload: true,
        preloadImage: 'images/download.gif',
        play: 5000,
        pause: 2500,
        hoverPause: true,
        animationStart: function (current) {
            $('.caption').animate({
                bottom: -35
            }, 100);
            if (window.console && console.log) {
                // example return of current slide number
                console.log('animationStart on slide: ', current);
            }
            ;
        },
        animationComplete: function (current) {
            $('.caption').animate({
                bottom: 0
            }, 200);
            if (window.console && console.log) {
                // example return of current slide number
                console.log('animationComplete on slide: ', current);
            }
            ;
        },
        slidesLoaded: function () {
            $('.caption').animate({
                bottom: 0
            }, 200);
        }
    });
});
$(function () {
    $("#topickey").val($("#q").val());
    $(".joingroup").click(function () { CheckUserIsJoinGroup(); })
    var type = $.getUrlParam("type");
    if (type == "" || type == null) {
        $("#tabAll").removeClass("tabmenu_b").addClass("tabmenu_a");
        $("#tabRecommand").removeClass("tabmenu_a").addClass("tabmenu_b");
        $("#tabUser").removeClass("tabmenu_a").addClass("tabmenu_b");
    }
    if (type == "User") {
        $("#tabAll").removeClass("tabmenu_a").addClass("tabmenu_b");
        $("#tabRecommand").removeClass("tabmenu_a").addClass("tabmenu_b");
        $("#tabUser").removeClass("tabmenu_b").addClass("tabmenu_a");
    }
    if (type == "Recommand") {
        $("#tabAll").removeClass("tabmenu_a").addClass("tabmenu_b");
        $("#tabRecommand").removeClass("tabmenu_b").addClass("tabmenu_a");
        $("#tabUser").removeClass("tabmenu_a").addClass("tabmenu_b");
    }
    $("#btnPostTopic").click(function () {
        if (CheckUserIsJoinGroup()) {
            return true;
        }
        return false;
    });
    $(".topicRecommand").die("click").live("click", function () {
        var ObjectThis = $(this);
        var TopicId = $(this).attr("topicid");
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxTopicOperation",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { Type: 1, TopicId: TopicId },
            success: function (resultData) {
                ObjectThis.hide().next().show();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    })
    $(".CancellTopicRecommand").die("click").live("click", function() {
        var ObjectThis = $(this);
        var TopicId = $(this).attr("topicid");
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxTopicOperation",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            data: { Type: 0, TopicId: TopicId },
            success: function(resultData) {
                ObjectThis.hide().prev().show();
            },
            error: function(XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    });
    $(".DeleteTopic").die("click").live("click", function () {
        var ObjectThis = $(this);
        var TopicId = $(this).attr("topicid");
        $.ajax({
            url: $Maticsoft.BasePath + "profile/AjaxTopicOperation",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { Type: 2, TopicId: TopicId },
            success: function (resultData) {
                if (resultData == "Ok") {
                    ObjectThis.parents(".content_post_one").hide();
                }
                else {
                    ShowFailTip("操作失败");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("操作失败：" + errorThrown);
            }
        });
    })
    $("#searchtopic").click(function () {
        var keyword = $("#topickey").val();
        window.location.href = $Maticsoft.BasePath + "Group/GroupInfo?GroupId=" + $("#groupid").val() + "&type=Search&q=" + keyword;
    })
})