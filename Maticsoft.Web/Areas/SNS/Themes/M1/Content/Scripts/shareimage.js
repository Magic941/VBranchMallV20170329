$(function () {
    $("#btnShareImage").click(function () {
        $.ajax({
            url: $Maticsoft.BasePath + "User/CheckUserState",
            type: 'post',
            dataType: 'text',
            timeout: 10000,
            success: function (resultData) {
                if (resultData != "Yes") {
                    $.jBox.tip('您还没有登录，请先登录！', 'success');
                    return false;
                } else {
                    //开始循环添加图片
                    var albumId = $("#txtAlbum").val();
                    var isSuccess = true;
                    if (albumId == -1 || albumId == "undefined") {
                        $.jBox.tip('请您先选择专辑！', 'success');
                        return false;
                    }
                    $.jBox.tip("正在上传图片，请稍候...", 'loading');
                    var list = [];
                    $(".image").each(function () {
                        var imageUrl = $(this).children().children().attr("src");
                        var imageAlt = $(this).children().children().attr("alt");
                        var shareDec = $(this).find('.mb5').val();
                        var json = { AlbumId: albumId, ImageUrl: imageUrl, ImageAlt: imageAlt, ShareDec: shareDec };
                        list.push(json);
                    });
                    $.ajax({
                        url: $Maticsoft.BasePath + "Home/AjaxAddImage",
                        type: 'post',
                        dataType: 'json',
                        data: { List: JSON.stringify(list) },
                        success: function (result) {
                            if (result.Data) {
                                $.jBox.tip('上传图片完成。', 'success');
                                $("#shareContent").hide();
                                $("#share_ok").show();
                                $(".txtAlbum").attr("href", "/Album/Details?AlbumID=" + albumId);
                                setTimeout("window.close();", 5000); 
                            } else {
                                $.jBox.tip('上传图片过程中出现错误。', 'error');
                                $("#shareContent").hide();
                                $("#share_error").show();
                                $(".txtAlbum").attr("href", "/Album/Details?AlbumID=" + albumId);
                                setTimeout("window.close();", 5000); 
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            $.jBox.tip('服务器繁忙，请稍候再试！', 'error');
                        }
                    });
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                ShowFailTip("服务器繁忙，请稍候再试！");
            }
        });
    });
});
        function btnHide(image) {
            $(image).parents("li").hide();
            $(image).parents("li").removeAttr("class");
        }
