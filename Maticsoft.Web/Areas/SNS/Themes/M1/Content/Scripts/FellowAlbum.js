var FavState = 0; //避免重复发送请求
function CheckIsFav(Obj, AlbumId) {
    var AlbumThis = $(Obj);
    $.ajax({
        type: "POST",
        dataType: "text",
        async: false,
        url: $Maticsoft.BasePath + "Album/AjaxCheckIsFav",
        data: { AlbumId: AlbumId },
        success: function (data) {
            if (data == "True") {
                AlbumThis.hide().next().show();
            }
        }
    });
}

function ReloadFellowAlbum() {
    $(".FellowAlbum").each(function () {
        if ($(this).attr("autotest") != "false" && $(this).attr("loaded") != 'ok') {
            CheckIsFav($(this), $(this).attr("AlbumId"));
            $(this).attr("loaded", 'ok');
        }
    });
    $(".FellowAlbum").unbind('click').bind('click', function (e) {
        if (CheckUserState()) {
            e.preventDefault();
            var AlbumId = $(this).attr("AlbumId");
            if (parseInt(AlbumId) == 0) {
                return;
            }
            var AlbumThis = $(this);

            $.ajax({
                type: "POST",
                dataType: "text",
                url: $Maticsoft.BasePath + "Album/AjaxFavAlbum",
                async: false,
                data: { AlbumId: AlbumId },
                success: function (data) {
                    if (data == "Fail") {
                        $.jBox.tip('亲，失败了....', 'success');
                    }
                    else {
                        AlbumThis.hide().next().show();
                    }
                }
            });
        }
    });

    $(".UnFellowAlbum").unbind('click').bind('click', function (e) {
        e.preventDefault();
        if (CheckUserState()) {
            var AlbumId = $(this).attr("AlbumId");
            var AlbumThis = $(this);
            $.ajax({
                type: "POST",
                dataType: "text",
                url: $Maticsoft.BasePath + "Album/AjaxUnFavAlbum",
                async: false,
                data: { AlbumId: AlbumId },
                success: function (data) {
                    if (data == "Fail") {
                        $.jBox.tip('亲，失败了....', 'success');
                    }
                    else {
                        AlbumThis.hide().prev().show();

                    }
                }
            });
        }
    });
}

$(function () {
    ReloadFellowAlbum();
});





