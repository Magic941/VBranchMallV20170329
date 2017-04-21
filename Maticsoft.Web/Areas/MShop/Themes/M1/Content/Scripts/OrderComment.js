//控制字数
function textCounter(thistext, maxlimit) {
    var content = $(thistext);
    var txtlength = parseInt(content.val().length);
    if (txtlength > maxlimit) {
        content.val(content.val().slice(0, 500));
    } else {
        content.prev('div').find('[name="shengyu"]').text(parseInt(maxlimit) - txtlength);
    }
}
//上传图片按钮
var qqupload = function (k) {
    var ulbtnparent = $("[name='UploadPhoto']").eq(k).parent();
    new qq.FineUploader({
        element: $("[name='UploadPhoto']")[k],
        request: {
            endpoint: '/UploadMultipleFileHandler.aspx'
        },
        text: {
            uploadButton: '晒照片'
        },
        multiple: true,
        validation: {
            allowedExtensions: ['jpeg', 'jpg', 'gif', 'png'],
            itemLimit: 5,
            sizeLimit: 5242880,
        },
        callbacks: {
            onComplete: function (id, fileName, responseJSON) {
                $(".qq-upload-list").hide();
                if (responseJSON.success) {
                    ulbtnparent.append(('<div style="display:inline-block;line-height: 45px;"><img src="{0}"  width="45px" height="45px"/><span  onclick="nameDel(this);"  item="{0}" itemname="{1}"  >删除</span></div>').format(
                       responseJSON.path.format(responseJSON.names), responseJSON.names));
                    ShowSuccessTip('上传成功！');
                    ulbtnparent.find('[name="UploadPhotoPath"]').val(ulbtnparent.find('[name="UploadPhotoPath"]').val() + '|' + responseJSON.path.format(responseJSON.names));
                    ulbtnparent.find('[name="UploadPhotoNames"]').val(ulbtnparent.find('[name="UploadPhotoNames"]').val() + '|' + responseJSON.names);
                    imghover();

                } else {
                    ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                }
            }
        }
    });
};
//鼠标移入移出图片
var imghover = function () {
    $('.reviewimg-upload').find('img').parent('div').unbind('hover').hover(function () {
        $(this).find('span').css('display', 'inline-block');
    }, function () {
        $(this).find('span').css('display', 'none');
    });
};
//删除图片
function nameDel(sender) {
    var ulbtnparent = $(sender).parents('.reviewimg-upload');
    var targetVal = $(sender).attr('item');
    $(sender).parent('div').remove();
    var pathArray = ulbtnparent.find('[name="UploadPhotoPath"]').val().split('|');
    var index = pathArray.getIndexByValue(targetVal);
    pathArray.remove(index);
    ulbtnparent.find('[name="UploadPhotoPath"]').val(pathArray.join('|'));

    var nameVal = $(sender).attr('itemname');
    var nameArray = ulbtnparent.find('[name="UploadPhotoNames"]').val().split('|');
    var indexname = nameArray.getIndexByValue(nameVal);
    nameArray.remove(indexname);
    ulbtnparent.find('[name="UploadPhotoNames"]').val(nameArray.join('|'));
}

//提交评论
var submit = function () {
    var json = []; //声明json
    for (var i = 0; i < $('.review_a').length; i++) {
        var contentval = $('[name="content"]').eq(i).val();
        if (contentval == "") {
            ShowFailTip("请填写评论内容！");
            return false;
        }
        if (contentval.length > 500) {
            ShowFailTip("评论内容过长！");
            return false;
        }
        var imagesurlPath = $('[name="UploadPhotoPath"]').eq(i).val();
        var imagesurlName = $('[name="UploadPhotoNames"]').eq(i).val();
        var attribute = $('[name="attribute"]').eq(i).val();
        var sku = $('[name="sku"]').eq(i).val();
        var pid = $('[name="pid"]').eq(i).val();
        var itemid = $('[name="orderId"]').eq(i).val();
        json.push({ "pid": pid, "orderId": itemid, "attribute": attribute, "sku": sku, "contentval": contentval, "imagesurlPath": imagesurlPath, "imagesurlName": imagesurlName });
    }
    $.ajax({
        url: $Maticsoft.BasePath + "UserCenter/AjAxPReview",
        type: 'post',
        dataType: 'text',
        async: false,
        timeout: 10000,
        data: { PReviewjson: JSON.stringify(json) },
        success: function (resultData) {
            switch (resultData) {
                case "false":
                    ShowServerBusyTip("提交失败");
                    break;
                case "0":
                    ShowSuccessTip("提交成功！");
                    setTimeout(function () {
                        window.location.replace($Maticsoft.BasePath + "UserCenter/Orders");
                    }, 1000);
                    break;
                default:
                    if (parseInt(resultData) > 0) {
                        ShowSuccessTip("提交成功！加(" + resultData + ")积分");
                        setTimeout(function () {
                            window.location.replace($Maticsoft.BasePath + "UserCenter/Orders");
                        }, 1000);
                    }
                    break;
            }
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
            ShowServerBusyTip("服务器没有返回数据，可能服务器忙，请稍候再试！");
        }
    });
}

//图片上传
$(function () {
    var uploadArr = $("[name='UploadPhoto']");

    for (var i = 0; i < uploadArr.length; i++) {
        qqupload(i);
    }
    //改变上传图片按钮的样式
    $('.qq-upload-button').css({ 'backgroundColor': '#A59D89', 'width': '55' });
});