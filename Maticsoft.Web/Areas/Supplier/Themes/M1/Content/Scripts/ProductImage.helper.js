//(function () {

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
            //商品图片不显示修改
            //img = img.format("T150X150_");
            img = img.format("T158X158_");
        } else {
            img = defaultImage;
        }
        $(this).find('.file_uploadUploader').css("background", backgtoundImgBase.format(img));
    });

    $('.file_upload').uploadify({
        'uploader': '/Admin/js/jquery.uploadify/uploadify-v2.1.4/uploadify.swf',   //指定上传控件的主体文件，默认‘uploader.swf’ 
        'script': '/ProductUploadImg.aspx',
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
        'onComplete': function (event, queueID, fileObj, response) {
          //var responseJSON  =$.parseJSON(response);
          //  if (responseJSON.success) {
          //      $(event.target).parent().css("background", backgtoundImgBase.format(responseJSON.data.format('T150X150_')));
          //      //url: /upload/{0}xxx.jpg
          //      $(event.target).parents('.ImgUpload').find("input[type=hidden]").val(responseJSON.data);
          //  }

            var responseJSON = $.parseJSON(response);
            if (responseJSON.success) {
                $(event.target).parent().css("background", backgtoundImgBase.format(responseJSON.data.format('T158X158_')));
                //url: /upload/{0}xxx.jpg
                $(event.target).parents('.ImgUpload').find("input[type=hidden]").val(responseJSON.data);
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

//} ());

