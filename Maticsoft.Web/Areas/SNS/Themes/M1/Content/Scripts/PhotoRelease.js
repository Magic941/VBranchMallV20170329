var ImageUrl = "";
var submitAddPhoto = function (v, h, f) {
    $.ajax({
        url: $Maticsoft.BasePath + "profile/AjaxPostAdd",
        type: 'post', dataType: 'text', timeout: 10000,
        data: { Type: "Photo", ImageUrl: ImageUrl, ShareDes: f.PhotoAddAlbumscontent, AblumId: f.myAlbums, ImageType: $("#UploadPhotoBtn").attr("imagetype")},
        success: function (resultData) {
            if (resultData == "No") {
                ShowFailTip("操作失败，请您重试！");
            }
            else {
                $.jBox.tip('分享成功', 'success');
            }
            return true;
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.jBox.tip('操作失败', 'success');
        }
    });
};
//function LoadAblumFun() {
//    $.ajax({
//        url: $Maticsoft.BasePath + "profile/AjaxGetMyMyAblum",
//        type: 'post', dataType: 'text', timeout: 10000,
//        success: function (resultData, textStatus, jqXHR) {
//            if (jqXHR.status == "801") {
//                return;
//            }
//            if (resultData == "No") {
//                ShowFailTip("操作失败，请您重试！");
//            }
//            else {
//                    var Datas = $.parseJSON(resultData);
//                    var html;
//                    for (var i = 0; i < Datas.length; i++) {
//                        html += "<option value=" + Datas[i].AlbumID + ">" + Datas[i].AlbumName + "</option>";
//                    }
//                    $("#myAlbums").html(html);
//            }
//        },
//        error: function (XMLHttpRequest, textStatus, errorThrown) {
//            ShowFailTip("操作失败：" + errorThrown);
//        }
//    });
//}
$(function() {
    // Upload($("#UploadPhotoBtn"), "Image");
  
});
    function Upload(control, type) {
        if (CheckUserState()) {
           
            var uploadbutton = $(control).html();
            var templatehtml = '<div class="qq-uploader span12">' +
            '<pre class="qq-upload-drop-area span12"><span>{dragZoneText}</span></pre>' +
            '<div class="qq-upload-button btn btn-success" style="width: auto;padding-top: 0px;auto: hidden;background:#fff;height: 50px">{uploadButtonText}</div>' +
            '<span class="qq-drop-processing"><span>{dropProcessingText}</span><span class="qq-drop-processing-spinner"></span></span>' +
            '<ul class="qq-upload-list" style="margin-top: 10px; text-align: center;"></ul>' +
            '</div>';

            var uploader = new qq.FineUploader({
                element: $(control)[0],
                request: {
                    endpoint: '/Upload/SNSUploadTmpImg.aspx'
                },
                text: {
                    uploadButton: uploadbutton
                },
                template: templatehtml,
                multiple: false,
                validation: {
                    allowedExtensions: ['jpeg', 'jpg', 'gif', 'png']
                },
                callbacks: {
                    onComplete: function (id, fileName, responseJSON) {
                        $(".btn-success").find("input").css("height", "28px");
                        if (responseJSON.success) {
                            $(".qq-upload-list").hide();
                            $.ajax({
                                url: $Maticsoft.BasePath + "profile/AjaxGetUploadPhotoResult",
                                type: 'post', dataType: 'text', timeout: 100000,
                                async: false,
                                success: function (resultData, textStatus, jqXHR) {
                                    $("#PhotoResultTemplate").html(resultData);
                                },
                                error: function (XMLHttpRequest, textStatus, errorThrown) {
                                    ShowFailTip("操作失败：" + errorThrown);
                                }
                            });
                            $.jBox.tip('上传成功', 'success');
                            ImageUrl = responseJSON.data;
                            $("#PhotoResultTemplate").find(".fabiao_a").find("img").attr("src", responseJSON.data.format("T116x170_"));
                            $("#PhotoResultTemplate").find(".createAlblums").hide();
                            $.jBox($("#PhotoResultTemplate").html(), { title: "加入专辑", buttons: { '加入': 1 }, submit: submitAddPhoto, width: 540, top: 300 });

                        } else {
                            ShowFailTip("服务器没有返回数据，可能服务器忙，请稍候再试：");
                        }
                    }
                }
            });
        }
}
//function OpenRelease(title, type) {
//    $.jBox.setDefaults({ defaults: { loaded: function (h) {
//        if (h.find("#myAlbums").length > 0) {
//            LoadAblumFun(h);
//            return;
//        }
//        if (h.find('#UploadPhoto').length < 1) return;
//        var fileType = "all", fileNum = "more";
//        new AjaxUpload(h.find('#UploadPhoto'), {
//            action: '/Ajax_Handle/SNSUploadPhoto.ashx',
//            data: {
//                'Type': 'Post'
//            },
//            name: 'myfile',
//            onSubmit: function (file, ext) {
//                $.jBox.tip("正在上传...", 'loading');
//                if (fileType == "pic") {
//                    if (ext && /^(jpg|png|jpeg|gif)$/.test(ext)) {
//                        this.setData({
//                            'info': '文件类型为图片'
//                        });
//                    } else {
//                        $.jBox.tip('请上传图片类型');
//                        return false;
//                    }
//                }
//                if (fileNum == 'one')
//                    this.disable();
//            },
//            onComplete: function (file, response) {
//                if (response == "-1") {
//                    $.jBox.tip('上传图片不能大于500k');
//                    return;
//                }
//                ImageUrl = response;
//                $.jBox.tip('上传成功', 'success');
//                $("#PhotoResultTemplate").find(".fabiao_a").find("img").attr("src", response.split("|")[2])
//                $.jBox($("#PhotoResultTemplate").html(), { title: "加入专辑", buttons: { '加入': 1 }, submit: submitAddPhoto, width: 540, top: 300 });
//                return false;
//                //ImageUrl = response;
//                //                $("#UploadPhotoWindow").hide();
//                //                if ($("#con_one_2").children().length < 2) {
//                //                    if ($("#con_one_3").children("#PhotoResultWindow")) {
//                //                        $("#con_one_3").children("#PhotoResultWindow").remove();
//                //                    }
//                //                    $("#con_one_2").append($("#PhotoResultTemplate").html().format(response, "Photo"));
//                //                }
//                //                else {
//                //                    $("#PhotoResultWindow").show();
//                //                }
//                //              $.jBox.nextState(); //go forward
//                //  $.jBox.goToState('state5');
//            }
//        });
//    }
//    }
//    });

////states.state5 = {
////    content: $("#PhotoResultTemplate").html(),
////    buttons: { '加入': 1} ,// no buttons
////    submit: submitAddPhoto,
////    width: 540,
////    top: 300
////};

//    var data = {};
//    var states = {};
//    states.state1 = {
//        content: $('#UploadPhotoTemplate').html(),
//        buttons: { '加入': 1 },
//        loaded: function (v, h, f) {
//            if (v == 0) {
//                return true; // close the window
//            }
//            else {
//                h.find('.errorBlock').hide('fast', function () { $(this).remove(); });

//                data.amount = f.amount; //或 h.find('#amount').val();
//                if (data.amount == '' || parseInt(data.amount) < 1) {
//                    $('<div class="errorBlock" style="display: none;">请输入购买数量！</div>').prependTo(h).show('fast');
//                    return false;
//                }
//                data.address = f.address;
//                if (data.address == '') {
//                    $('<div class="errorBlock" style="display: none;">请输入收货地址！</div>').prependTo(h).show('fast');
//                    return false;
//                }

//            }

//            return false;
//        }
//    };
//    states.state2 = {
//        content: $('#PhotoResultTemplate').html(),
//        buttons: { '上一步': -1, '提交': 1, '取消': 0 },
//        buttonsFocus: 1, // focus on the second button
//        submit: function (v, o, f) {
//            if (v == 0) {
//                return true; // close the window
//            } else if (v == -1) {
//                $.jBox.prevState() //go back
//                // 或 $.jBox.goToState('state1');
//            }
//            else {
//                data.message = f.message;

//                // do ajax request here
//                $.jBox.nextState('<div class="msg-div">正在提交...</div>');
//                // 或 $.jBox.goToState('state3', '<div class="msg-div">正在提交...</div>')

//                // asume that the ajax is done, than show the result
//                var msg = [];
//                msg.push('<div class="msg-div">');
//                msg.push('<p>下面是提交的数据</p>');
//                for (var p in data) {
//                    msg.push('<p>' + p + ':' + data[p] + '</p>');
//                }
//                msg.push('</div>');
//                window.setTimeout(function () { $.jBox.nextState(msg.join('')); }, 2000);
//            }

//            return false;
//        }
//    };
//    states.state3 = {
//        content: '',
//        buttons: {} // no buttons
//    };
//    states.state4 = {
//        content: '',
//        buttons: { '确定': 0 }
//    };
//    $.jBox.open(states, title, 'auto', 'auto');
//}

