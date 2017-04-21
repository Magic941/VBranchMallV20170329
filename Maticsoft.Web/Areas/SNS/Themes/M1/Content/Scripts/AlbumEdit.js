function sub() {
    if (!$('#AlbumName').val()) {
        $.jBox.tip('请输入名称');
        return false;
    }
    return true;
}
var submit = function (v, h, f) {
    if (v == 'ok') {
        $.jBox.tip("正在删除数据...", 'loading');
        var AlbumId = $("#delAlbum").attr("AlbumId");
        $.ajax({
            url: $Maticsoft.BasePath + "Profile/AjaxDelAlbum",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { AlbumId: AlbumId },
            success: function (resultData) {
                if (resultData == "True") {
                    $.jBox.tip('删除成功。', 'success');
                    window.location = $Maticsoft.BasePath + "Profile/Albums";
                }
                else {
                    $.jBox.tip('操作失败', 'success');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.jBox.tip('操作失败', 'success');
            }
        });
    }
    else if (v == 'cancel') {
        // 取消
    }
    return true; //close
};
$(function () {
    $("#delAlbum").click(function () {
        $.jBox.confirm("确定要删除此专辑和专辑下面的商品或图片吗？", "提示", submit);
    });
})