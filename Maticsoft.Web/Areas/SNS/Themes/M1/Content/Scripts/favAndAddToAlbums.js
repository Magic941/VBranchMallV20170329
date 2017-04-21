
$(".jbox-button").hide();
//加入专辑的弹出框
var submitAddAlbum = function (v, h, f) {
    var html;
    if (f.albumid == undefined || f.albumid <= 0) {
        $.jBox.tip('请先选择专辑！', 'fail');
    }
    else {
        $.ajax({
            url: $Maticsoft.BasePath + "Profile/AjaxAddToAlbum",
            type: 'post', dataType: 'text', timeout: 10000,
            data: { TargetId: f.targetid, Type: f.imagetype, Des: f.AddAlbumscontent, AlbumId: f.albumid },
            success: function (resultData) {
                if (resultData == "Repeat") {
                    $.jBox.tip('不能重复加入哦', 'success');
                }
                else if (resultData == "No") {
                    $.jBox.tip('加入失败，请重试', 'success');
                }
                else {
                    $.jBox.tip('成功加入', 'success');
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                $.jBox.tip('加入失败，请重试', 'success');
            }
        });
        return true;
    }
};

var AJaxGetUserId = function() {
    var userid = 0;
    $.ajax({
        url: $Maticsoft.BasePath + "Profile/AjaxGetUserId",
        type: 'post',
        dataType: 'text',
        timeout: 10000,
        success: function(resultData) {
            alert(resultData);
            userid = resultData;
            return resultData;
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            return 0;
        }
    });
    return userid;

};
//添加喜欢所触发的事件
$(".favourite").die("click").live("click", function (e) {
    e.preventDefault();
    if (CheckUserState()) {
        var Type = $(this).attr("imagetype");
        var TargetID = $(this).attr("targetid");
        var NowID = $(this);
        var Number = NowID.parent().next().children("a");
        var TopicId = NowID.attr("topicid");
        var ReplyId = NowID.attr("replyid");
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Profile/AjaxAddFavourite",
            data: { Type: Type, TargetId: TargetID, TopicId: TopicId, ReplyId: ReplyId },
            success: function (data) {
                if (data == "No") {
                    $.jBox.tip('操作出错', 'success');
                }
                else if (data == "Repeat")
                    $.jBox.tip('您已经喜欢过了', 'success');
                else {
                    Number.text(parseInt(Number.text()) + 1);

                }
            }
        });
    }
})
//加入专辑需要的事件
$(".addalbum").die("click").live("click", function (e) {
    e.preventDefault();
    if (CheckUserState()) {
        var url = $(this).attr("imageurl");
        var imagetype = $(this).attr("imagetype");
        var targetid = $(this).attr("targetid");
        $.ajax({
            type: "POST",
            dataType: "text",
            url: $Maticsoft.BasePath + "Album/GetUserAlbums",
            success: function (data) {
                var Datas = $.parseJSON(data);
                var ResultHtml = "<select style='width:200px' id='albumid' name='albumid'>";
                if (data != "No") {
                    for (var i = 0; i < Datas.length; i++) {
                        ResultHtml += " <option value =" + Datas[i].AlbumID + ">" + Datas[i].AlbumName + "</option>";
                    }
                    ResultHtml += "</select> <a  href='" + $Maticsoft.BasePath + "Album/Create' target='_blank'>创建</a>";
                    ResultHtml += "<input type='hidden' name='imagetype' value='" + imagetype + "' />";
                    ResultHtml += "<input type='hidden' name='targetid' value='" + targetid + "' />";
                    $("#albumsinfo").html(ResultHtml);
                    $("#addToablum").find("#addtoimage").attr("src", url);
                    var html = $("#addToablum").html();
                    $.jBox(html, { title: "加入专辑", buttons: { '加入': 1 }, submit: submitAddAlbum, width: 550, top: 300 });
                }
            }
        });

    }
});