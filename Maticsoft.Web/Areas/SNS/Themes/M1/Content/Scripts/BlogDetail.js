var Type, TargetId, ShareDes;
$(function () {
    $("#biaoqingclose").die("click").live("click", function () {
        $("#tbiaoqing").hide();
    });
    $("#biaoqingshow").click(function (e) {
        e.preventDefault();
        $("#tbiaoqing").slideToggle(0);
    });
    $(".replycomment").die("click").live("click", function (e) {
        e.preventDefault();
        $("#CommentContent").val("@" + $(this).attr("nickname") + " " + ":");
    });
    $("#btnBlogFav").click(function () {
        var TargetId = $("#TargetId").val();
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Blog/AjaxFavCount",
            async: false,
            data: { BlogId: TargetId },
            success: function (data) {
                if (data == "Repeat") {
                    ShowSuccessTip("您已经喜欢过了！");
                }
                else if (data == "Yes") {
                    ShowSuccessTip("喜欢成功！");
                    var count = parseInt($("#favouritecount").text());
                    $("#favouritecount").text(count + 1);

                } else {
                    ShowFailTip("服务器繁忙，请稍候再试！");
                }
            }
        });
    });
    $("#AddCommentBtn").click(function (e) {
        e.preventDefault();
        if (CheckUserState()) {
            if ($("#CommentContent").val().length <= 0) {
                return;
            }
            Type = "Blog";
            var TargetId = $("#TargetId").val();
            ShareDes = $("#CommentContent").val();
            $.ajax({
                type: "POST",
                dataType: "text",
                url: $Maticsoft.BasePath + "Partial/AjaxAddComment",
                async: false,
                data: { Type: Type, TargetId: TargetId, Des: $("#CommentContent").val() },
                success: function (data) {
                    if (data != "No") {
                        var comment = $(data);
                        comment.hide();
                        $("#CommentArea").prepend(data);
                        $("#CommentContent").val("");
                        var count = parseInt($("#CommentCount").text());
                        $("#CommentCount").text(count+1);
                        $("#commmentcount").text(parseInt($("#commmentcount").text()) + 1);
                        comment.slideToggle();
                    } else {
                        ShowFailTip("操作失败：" + errorThrown);
                    }
                }
            });
            var mediaIds = "";
            if ($(this).parents(".tgoods_rep_sa2_b").find(".isSendAll").attr('checked') != undefined) {
                mediaIds = "-1";
            } else {
                var i = 0;
                $(this).parents(".tgoods_rep_sa2_b").find(".bind>span").each(function () {
                    if ($(this).attr("s_type") == "1" && $(this).attr("value") != "") {
                        if (i == 0) {
                            mediaIds = $(this).attr("value");
                        } else {
                            mediaIds = mediaIds + "," + $(this).attr("value");
                        }
                        i++;
                    }
                });
            }
            var ImageUrl = $("#ImageUrl").val();
            //同步到微博
            var Option = {
                Type: Type,
                ShareDes: ShareDes,
                ImageUrl: ImageUrl,
                TargetID: TargetId,
                mediaIds: mediaIds
            };
            InfoSync.InfoSending(Option);
        }
    });

});

function insertsmilie(smilieface) {
    $("[id$='CommentContent']").val($("[id$='CommentContent']").val() + smilieface);
    $("#tbiaoqing").hide();
    if (event && event.stopPropagation)
    //因此它支持W3C的stopPropagation()方法 
        event.stopPropagation();
    else
    //否则，我们需要使用IE的方式来取消事件冒泡 
        window.event.cancelBubble = true;


}
$(function () {
    //获取评论数
    var Commentcount = 0;
    var CommentPageSize = $("#CommentPageSize").val();
    var TargetId = $("#TargetId").val();
    //获取PvCount数
    $.ajax({
        type: "POST",
        dataType: "text",
        url: $Maticsoft.BasePath + "Blog/AjaxGetPvCount",
        data: { id: TargetId },
        success: function (data) {
            $(".txtBlogPvCount").text(data);
        }
    });

    $.ajax({
        type: "POST",
        dataType: "text",
        async: false,
        url: $Maticsoft.BasePath + "Blog/AjaxCount",
        data: { BlogId: TargetId },
        success: function (data) {
            Commentcount = parseInt(data.split("|")[1]);
            $("#CommentCount").text(Commentcount);
            $("#favouritecount").text(data.split("|")[0]);
        }
    });

    if (parseInt(Commentcount) <= parseInt(CommentPageSize)) {
        $("#Pagination").hide();

    }
    var initPagination = function () {
        var num_entries = Commentcount;
        $("#Pagination").pagination(num_entries, {
            num_edge_entries: 1,
            num_display_entries: 10,
            callback: pageselectCallback,
            items_per_page: CommentPageSize,
            prev_text: "前一页",
            next_text: "后一页",
            link_to: "javascript:void(0)"
        });
    };
    initPagination();
    function pageselectCallback(page_id, jq) {
        $("#CommentArea").html("<div style='margin:0 auto;text-align: center;'><img src='/Areas/SNS/Themes/M1/Content/images/ui.loading.gif' ></div>");
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Partial/AjaxGetCommentsByType",
            data: { pageIndex: page_id + 1, type: 'Blog', pid: TargetId },
            success: function (data) {
                if (data != "No") {
                    $("#CommentArea").html(data);
                } else {
                    ShowFailTip("操作失败：" + errorThrown);
                }
            }
        });
    }

});

