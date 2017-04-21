var Type, TargetId, ShareDes;
$(function () {
    $("#biaoqingclose").die("click").live("click", function () { $("#tbiaoqing").hide(); });
    $("#biaoqingshow").click(function (e) {
        e.preventDefault();
        $("#tbiaoqing").slideToggle(0);
    });
    $(".replycomment").die("click").live("click", function (e) {
        e.preventDefault();
        $("#CommentContent").val("@" + $(this).attr("nickname") + " " + ":");
    });
    $("#AddCommentBtn").click(function (e) {
        e.preventDefault();
        if (CheckUserState()) {
            if ($("#CommentContent").val().length <= 0) {
                return;
            }
            Type = $(this).attr("detailtype");
            TargetId = $(this).attr("targetid");
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
    event.stopPropagation();

}


//获取评论
$(function () {
//添加喜欢
    $("#videofav").click(function () {
        var Type = "Video";
        var TargetID = $(this).attr("targetid");
        var NowID = $(this);
        var Number = NowID.next();
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Profile/AjaxAddFavourite",
            data: { TargetId: TargetID, Type: Type },
            success: function (data) {
                if (data == "No") {
                    $.jBox.tip('操作出错', 'success');
                } else if (data == "Repeat")
                    $.jBox.tip('您已经喜欢过了', 'success');
                else {
                    Number.text(parseInt(Number.text()) + 1);
                    $("#favcount").text(parseInt(Number.text()));
                    $.jBox.tip('恭喜你，喜欢成功！', 'success');
                }
            }
        });
    });


    var Commentcount = $("#Commentcount").val();
    var CommentPageSize = $("#CommentPageSize").val();
    var CommentType = "Video";
    var PostID = $("#PostID").val();
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
            data: { pageIndex: page_id + 1, type: CommentType, pid: PostID },
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
