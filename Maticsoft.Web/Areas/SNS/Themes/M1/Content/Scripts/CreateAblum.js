var typeid;
$(function () {
    $("#createAlbum").click(function() {
        $.ajax({
            type: "POST",
            dataType: "text",
            async: false,
            url: $Maticsoft.BasePath + "Profile/AjaxCreateAlbums",
            success: function(data) {
                $(data).find('input:radio:first').attr('checked', 'checked');
                var html = $(data).find("#CreateAlbums").html();
                $(data).find('.cre_a_2b input:first').attr('checked', 'true');
                $.jBox(html, { title: "新建专辑", buttons: { '创建': 1 }, submit: submitAlbum, width: 400, top: 300 });
            }
        });
    });
});
var submitAlbum = function (v, h, f) {
    if (f.AlbumName == '') {
        $.jBox.tip('请填写专辑的名称', 'success');
        return;
    }
    if (!f.albumtype) {
        $.jBox.tip('请选择类型', 'success');
        return;
    }
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxAddAlbum",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { AlbumName: f.AlbumName, Type: f.albumtype },
        success: function (resultData) {
             if (resultData == "NO") {
                $.jBox.tip('出现异常');
            }
            else if (resultData == "NoLogin") {
                $.jBox.tip('您还没有登录, 请刷新页面, 并登录后再试!');
            }
            else if (resultData == "AA") {
                $.jBox.tip('管理员不能创建专辑, 请您更换普通帐号再试!');
            }
           else {
                $.jBox.tip('创建成功,请刷新', 'success');
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            ShowFailTip("操作失败：" + errorThrown);
        }
    });

    return true;
};