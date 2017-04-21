/**
* $itemname$.js
*
* 功 能： [N/A]
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  $time$  $username$    初版
*
* Copyright (c) $year$ Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

var defaultImage = '/admin/images/AddImage.gif';
var backgtoundImgBase = "url('{0}') no-repeat center center";
var ThumbSize = {};

$(document).ready(function () {

    // -------- Set ThumbSize START -----------
    if ($('[id$=hfProductImagesThumbSize]').val()) {
        var tmpThumbSize = $('[id$=hfProductImagesThumbSize]').val().split(',');
        if (tmpThumbSize.length == 2) {
            ThumbSize.Width = parseInt(tmpThumbSize[0]);
            ThumbSize.Height = parseInt(tmpThumbSize[1]);
        }
    }

    $('.product_upload_img_ul li span').width(ThumbSize.Width);
    $('.product_upload_img_ul .cancel,.product_upload_img_ul .cancel_pop').width(ThumbSize.Width);
    // -------- Set ThumbSize END ------------

    $('.ImgUpload').each(function () {
        var img = $(this).find("input[type=hidden]").val();
        if (img) {
            img = img.format("T_");
        } else {
            img = defaultImage;
        }
        $(this).find('.file_uploadUploader').css("background", backgtoundImgBase.format(img));
    });

    $('.file_upload').uploadify({
        'uploader': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.swf',   //指定上传控件的主体文件，默认‘uploader.swf’ 
        'script': '/GiftImgUpload.aspx',
        'cancelImg': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/cancel.png',   //指定取消上传的图片，默认‘cancel.png’ 
        'wmode': 'transparent',
        'width': ThumbSize.Width,
        'height': ThumbSize.Height,
        'hideButton': true,
        'auto': true,               //选定文件后是否自动上传，默认false 
        'multi': false,               //是否允许同时上传多文件，默认false 
        'fileDesc': '图片文件', //出现在上传对话框中的文件类型描述 
        'fileExt': '*.jpg;*.bmp;*.png;*.gif',      //控制可上传文件的扩展名，启用本项时需同时声明fileDesc 
        'sizeLimit': 2097152,          //控制上传文件的大小，单位byte 
        'onComplete': function (event, queueID, fileObj, response, data) {
            if (response.split('|')[0] == "1") {
                $(event.target).parent().css("background", backgtoundImgBase.format(response.split('|')[1].format('T_')));
                //url: /upload/{0}xxx.jpg
               // $(event.target).parents('.ImgUpload').find("input[type=hidden]").val(response.split('|')[1]);
                $("#hfImage").val(response.split('|')[1]);
                $("#thfImage").val(response.split('|')[1].format('T_'));
            }
        },
        //                    'onError': function(event, queueID, fileObj) {
        //                        alert("文件:" + fileObj.name + " 上传失败");
        //                    }
        'onError': function (event, ID, fileObj, errorObj) {
            //            alert('上传图片大小不能超过2M，尺寸不能大于1280×1280');
            alert('上传文件发生错误, 状态码: [' + errorObj.info + ']');
        }
    });

    $(".cancel,.file_uploadUploader").hover(function () {
        if ($(this).parents('.ImgUpload').find("input[type=hidden]").val() != "") {
            $(this).parents('.ImgUpload').find(".cancel").css('display', '');
        }
    }, function () {
        $(this).parents('.ImgUpload').find(".cancel").css('display', 'none');
    });

    $(".DelImage").click(function () {
        //删除图片
        $(this).parents('.ImgUpload').find(".file_uploadUploader").css("background", backgtoundImgBase.format(defaultImage));
        $(this).parents('.ImgUpload').find("input[type=hidden]").val("");
        $(this).parents('.ImgUpload').find(".cancel").css('display', 'none');
    });

});