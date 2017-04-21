//获取会员身高，本重，等信息
var ShareTemplate = {
    arCommentIds: null,
    Init: function () {
        BehaviorTemplate.arCommentIds = new Array();
    },
    SyncFunction: function () {
        ShareTemplate.UpdateCommentInfo(commentIds.join(",")); //异步更新评论认为有用数等
    },
    UpdateCommentInfo: function (commentIds, obj) //异步更新评论认为有用数等
    {
        if (commentIds.length > 0) {
            $.ajax({
                type: "GET",
                contentType: "application/json; charset=UTF-8",
                url: "Http://myshishang.maticsoft.com/Service/AjaxService.ashx",
                data: "commentIds=" + commentIds + "&Method=GetShortCommentsInfo",
                dataType: "jsonp",
                success: function (result) {
                    try {
                        if (result.Data != null) {
                            $(result.Data).each(function (i, n) {

                                //获得评论中身高，体重等信息
                                var objNowShapeDiv = $("div[shape='hw_" + n.CommentId + "'] p");
                                objNowShapeDiv.each(function (i, r) {
                                    $(r).empty();
                                    var IsShow = false;

                                    if (n.Height > 0) {
                                        $(r).append("<span>身高：" + n.Height + "厘米</span>");
                                        IsShow = true;

                                    }

                                    if (n.Weight > 0) {
                                        $(r).append("<span>体重：" + n.Weight + "公斤</span>");
                                        IsShow = true;
                                    }

                                    if (n.StyleColor.length > 0) {
                                        $(r).append("<span>颜色：" + n.StyleColor + "</span>");
                                        IsShow = true;
                                    }

                                    if (n.StyleSize.length > 0) {
                                        $(r).append("<span>尺码：" + n.StyleSize + "码</span>");
                                        IsShow = true;
                                    }
                                    if (IsShow) {
                                        $(r).parent().show();
                                    }
                                });
                            });
                        }
                    } catch (e) { }
                }
            });
        }
    },
    InitFavoriteCount: function () {
        var styleIds = "";
        $(".FavoriteCount_Span").each(function () {
            styleIds += $(this).attr("styleid") + ",";
        });
        if (styleIds.length > 1) styleIds = styleIds.substring(0, styleIds.length - 1);
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=UTF-8",
            url: "Http://myshishang.maticsoft.com/Service/AjaxService.ashx",
            data: "styleIds=" + styleIds + "&Method=GetShortProductsInfo",
            dataType: "jsonp",
            async: false,
            success: function (data) {
                $(".FavoriteCount_Span").each(function () {
                    for (var i in data.Data) {
                        if (data.Data[i].StyleId == $(this).attr("styleid")) {
                            if (data.Data[i].FavoriteCount == null) {
                                $(this).html("0");
                            } else {
                                $(this).html(data.Data[i].FavoriteCount);
                            }
                            break;
                        }
                        continue;
                    }
                });
            },
            error: function () {
                //如果AJAX失败将不显示该模块
                $(".daren_share_div").hide();
            }
        });
    }
}