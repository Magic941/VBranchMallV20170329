$(function () {
    $(".replycomment").die("click").live("click", function (e) {
        e.preventDefault();
        $("#CommentContent").val("@" + $(this).attr("nickname") + " " + ":");
    });
    $("#AddCommentBtn").click(function (e) {
        e.preventDefault();
        if (CheckUserState()) {
            var TargetId = $("#AddCommentBtn").attr("TargetId");
            var desc = $("#CommentContent").val();
            if (desc.length <= 0) {
                $.jBox.tip('请填写内容', 'error');
                return;
            }
            if (ContainsDisWords(desc)) {
                $.jBox.tip('您输入的内容含有禁用词，请重新输入！', 'error');
                return;
            }
            $.ajax({
                type: "POST",
                dataType: "text",
                url: $Maticsoft.BasePath + "Album/AjaxAddComment",
                data: { TargetId: TargetId, Des: $("#CommentContent").val() },
                success: function (data) {
                    if (data != "No") {

                        var Option = {
                            AlbumID: TargetId,
                            ShareDes: $("#CommentContent").val()
                        };
                        InfoSync.InfoSending(Option);

                        var comment = $(data);
                        comment.hide();
                        $("#CommentArea").prepend(data);
                        comment.slideToggle();
                        $("#CommentContent").val("");
                        //同步到微博

                    } else {
                        $.jBox.tip('操作失败', 'success');
                    }
                }
            });
        }

    });
});
function insertsmilie(smilieface) {
    $("[id$='CommentContent']").val($("[id$='CommentContent']").val() + smilieface);
    $("#tbiaoqing").hide();
}

$(function () {
    var CommentCount = $("#CommentCount").val();
    var CommentPageSize = $("#CommentPageSize").val();
    var AlbumID = $("#AlbumID").val();
    $("#biaoqingclose").die("click").live("click", function () { $("#tbiaoqing").hide(); })
    $("#biaoqingshow").click(function (e) { e.preventDefault(); $("#tbiaoqing").slideToggle(0) });
    if (parseInt( CommentCount) <= parseInt( CommentPageSize)) {
        $("#Pagination").hide();
    }
    var initPagination = function () {
        var num_entries = CommentCount;
        $("#Pagination").pagination(num_entries, {
            num_edge_entries: 1,
            num_display_entries: 3,
            callback: pageselectCallback,
            items_per_page: CommentPageSize,
            prev_text: "",
            next_text: "",
            prev_show_always: false,
            next_show_always: false,
            link_to: "javascript:void(0)"
        });
    };
    initPagination();
    function pageselectCallback(page_id, jq) {
        $("#CommentArea").html("<div style='margin:0 auto;text-align: center;'><img src='/Areas/SNS/Themes/M1/Content/images/ui.loading.gif' ></div>");
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Album/AjaxGetComments",
            data: { pageIndex: page_id + 1, AlbumId: AlbumID },
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